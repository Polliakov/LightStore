using LightStore.Application.Dtos.Order;
using LightStore.Application.Dtos.ProductInOrder;
using LightStore.Persistence.Entities;
using Mapster;

namespace LightStore.Application.Mapping.Mappers
{
    [Mapper]
    public interface IOrderMapper
    {
        OrderDetailsVm MapToDetailsDto(Order order);
        CustomersOrderDetailsVm MapToCustomersDetailsDto(Order order);
        OrderVm MapToDto(Order order);
        Order MapFromDto(CreateOrderDto dto);
    }
}
