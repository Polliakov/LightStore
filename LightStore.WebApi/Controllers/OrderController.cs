using LightStore.Application.Dtos.Order;
using LightStore.Application.Dtos.Pagination;
using LightStore.Application.Dtos.ProductInOrder;
using LightStore.Application.Services;
using LightStore.ImageService.Interfaces;
using LightStore.Persistence.Entities.Base;
using LightStore.WebApi.Attributes;
using LightStore.WebApi.Dtos.Order;
using LightStore.WebApi.Dtos.ProductInOrder;
using LightStore.WebApi.Mapping.Mappers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightStore.WebApi.Controllers
{
    public class OrderController : BaseController
    {
        public OrderController(IOrderService orderService,
                               IImageService imageService,
                               IOrderMapper orderMapper,
                               IProductInOrderMapper productInOrderMapper)
        {
            this.orderService = orderService;
            this.imageService = imageService;
            this.orderMapper = orderMapper;
            this.productInOrderMapper = productInOrderMapper;
        }

        private readonly IOrderService orderService;
        private readonly IImageService imageService;
        private readonly IOrderMapper orderMapper;
        private readonly IProductInOrderMapper productInOrderMapper;

        [HttpGet("{orderId}")]
        [AuthorizeRole(AppUserRole.Employee)]
        public async Task<ActionResult<OrderDetailsResponse>> GetDetails(Guid orderId)
        {
            var vm = await orderService.GetDetails(orderId);

            var response = orderMapper.MapToDetailsResponse(vm);
            response.ProductsInOrder = ProductsVmToResponse(vm.ProductsInOrder);

            return Ok(response);
        }

        [HttpGet("ForCustomer/{orderId}")]
        [AuthorizeRole(AppUserRole.Customer)]
        public async Task<ActionResult<CustomersOrderDetailsResponse>> GetCustomersOrderDetails(Guid orderId)
        {
            var vm = await orderService.GetCustomersOrderDetails(UserId, orderId);

            var response = orderMapper.MapToCustomersDetailsResponse(vm);
            response.ProductsInOrder = ProductsVmToResponse(vm.ProductsInOrder);

            return Ok(response);
        }

        [HttpGet]
        [AuthorizeRole(AppUserRole.Employee)]
        public async Task<ActionResult<OrdersPageVm>> GetOrdersPage(
            int page, int pageSize)
        {
            var ordersPage = await orderService.GetPage(
                new PaginationArgs(page, pageSize)
            );
            return Ok(ordersPage);
        }

        [HttpGet("ForCustomer")]
        [AuthorizeRole(AppUserRole.Customer)]
        public async Task<ActionResult<List<OrderVm>>> GetCustomersOrders()
        {
            var vms = await orderService.GetCustomersOrders(UserId);
            return Ok(vms);
        }

        [HttpPost]
        [AuthorizeRole(AppUserRole.Customer)]
        public async Task<ActionResult<Guid>> CreateOrder([FromBody] CreateOrderDto dto)
        {
            var vm = await orderService.Create(UserId, dto);
            return Ok(vm);
        }

        [Obsolete]
        [HttpPatch("StatusChange")]
        [AuthorizeRole(AppUserRole.Employee)]
        public async Task<IActionResult> ChangeStatus([FromBody] ChangeOrderStatusDto dto)
        {
            await orderService.ChangeStatus(dto);
            return NoContent();
        }

        private IEnumerable<ProductInOrderResponse> ProductsVmToResponse(List<ProductInOrderVm> vms)
        {
            if (vms is null) return null;

            return vms.Select(vm => productInOrderMapper.MapToResponse((
                vm,
                imageService.GetImageUri(vm.ProductId)
            )));
        }
    }
}
