using LightStore.Application.Dtos.Order;
using LightStore.Application.Dtos.Pagination;
using LightStore.Application.Dtos.ProductInOrder;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LightStore.Application.Services
{
    public interface IOrderService
    {
        Task<OrderDetailsVm> GetDetails(Guid orderId);
        Task<CustomersOrderDetailsVm> GetCustomersOrderDetails(Guid customerId, Guid orderId);
        Task<OrdersPageVm> GetPage(PaginationArgs pagination);
        Task<List<OrderVm>> GetCustomersOrders(Guid customerId);
        Task<Guid> Create(Guid customerId, CreateOrderDto dto);
        [Obsolete]
        Task ChangeStatus(ChangeOrderStatusDto dto);
    }
}
