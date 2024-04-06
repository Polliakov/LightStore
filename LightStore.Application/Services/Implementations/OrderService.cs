using LightStore.Application.Dtos.Order;
using LightStore.Application.Dtos.Pagination;
using LightStore.Application.Dtos.ProductInOrder;
using LightStore.Application.Exceptions;
using LightStore.Application.Mapping.Mappers;
using LightStore.Application.Utils;
using LightStore.Persistence.Entities;
using LightStore.Persistence.Entities.Base;
using LightStore.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LightStore.Application.Services.Implementations
{
    public class OrderService : BaseDataService, IOrderService
    {
        public OrderService(IProductInOrderService productInOrderService,
                            IProductInWarehouseService productInWarehouseService,
                            ICartService cartService,
                            IOrderMapper orderMapper,
                            ILightStoreDbContext dbContext)
            : base(dbContext)
        {
            this.productInOrderService = productInOrderService;
            this.productInWarehouseService = productInWarehouseService;
            this.cartService = cartService;
            this.orderMapper = orderMapper;
        }

        private readonly IProductInOrderService productInOrderService;
        private readonly IProductInWarehouseService productInWarehouseService;
        private readonly ICartService cartService;
        private readonly IOrderMapper orderMapper;

        public async Task<OrderDetailsVm> GetDetails(Guid orderId)
        {
            var order = await dbContext.Orders.AsNoTracking()
                .Include(o => o.Warehouse)
                .Include(o => o.DeliveryInformation)
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.Id == orderId)
                ?? throw new NotFoundException(nameof(Order), orderId);

            order.ProductsInOrder = await GetItems(orderId);

            return orderMapper.MapToDetailsDto(order);
        }

        public async Task<CustomersOrderDetailsVm> GetCustomersOrderDetails(Guid customerId, Guid orderId)
        {
            var order = await dbContext.Orders.AsNoTracking()
                .Include(o => o.Warehouse)
                .Include(o => o.DeliveryInformation)
                .FirstOrDefaultAsync(o => o.Id == orderId && o.CustomerId == customerId)
                ?? throw new NotFoundException(nameof(Order), (orderId, customerId));

            order.ProductsInOrder = await GetItems(orderId);

            return orderMapper.MapToCustomersDetailsDto(order);
        }

        public async Task<OrdersPageVm> GetPage(PaginationArgs pagination)
        {
            var query = dbContext.Orders.AsNoTracking();
            var foundCount = await query.CountAsync();

            var orders = await query
                .OrderByDescending(o => o.CreationDate)
                .Paginate(pagination)
                .Include(o => o.Warehouse)
                .Include(o => o.DeliveryInformation)
                .Select(o => orderMapper.MapToDto(o))
                .ToListAsync();

            return new OrdersPageVm { FoundCount = foundCount, Orders = orders };
        }

        public async Task<List<OrderVm>> GetCustomersOrders(Guid customerId)
        {
            return await dbContext.Orders.AsNoTracking()
                .Where(o => o.CustomerId == customerId)
                .OrderByDescending(o => o.CreationDate)
                .Include(o => o.Warehouse)
                .Include(o => o.DeliveryInformation)
                .Select(o => orderMapper.MapToDto(o))
                .ToListAsync();
        }

        public async Task<Guid> Create(Guid customerId, CreateOrderDto dto)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                await productInWarehouseService.Writeoff(dto.ProductsInOrder, dto.WarehouseId);
                await cartService.RemoveOrderedItems(dto.ProductsInOrder, customerId);
                var order = await CreateOrderInstance(customerId, dto);

                await dbContext.Orders.AddAsync(order);
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return order.Id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        [Obsolete]
        public async Task ChangeStatus(ChangeOrderStatusDto dto)
        {
            var order = await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == dto.Id)
                ?? throw new NotFoundException(nameof(Order), dto.Id);
            order.Status = dto.Status;
            await dbContext.SaveChangesAsync();
        }

        private async Task<Order> CreateOrderInstance(Guid customerId, CreateOrderDto dto)
        {
            var order = orderMapper.MapFromDto(dto);
            order.Id = Guid.NewGuid();
            order.CustomerId = customerId;
            order.Status = OrderStatus.Created;
            order.CreationDate = DateTime.UtcNow;

            order.ProductsInOrder = await productInOrderService
                .LinkProductsInOrder(order.Id, order.ProductsInOrder);

            if (order.DeliveryInformation is not null)
            {
                order.DeliveryInformation.Id = Guid.NewGuid();
                order.DeliveryInformationId = order.DeliveryInformation.Id;
            }
            return order;
        }

        private async Task<List<ProductInOrder>> GetItems(Guid orderId)
        {
            return await dbContext.ProductsInOrders.AsNoTracking()
                .Where(pio => pio.OrderId == orderId)
                .Include(pio => pio.ActualPrice)
                .Include(pio => pio.Product)
                .ToListAsync();
        }
    }
}
