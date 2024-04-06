using LightStore.Application.Dtos.Order;
using LightStore.WebApi.Dtos.Order;
using Mapster;

namespace LightStore.WebApi.Mapping.Mappers
{
    [Mapper]
    public interface IOrderMapper
    {
        OrderDetailsResponse MapToDetailsResponse(OrderDetailsVm vm);
        CustomersOrderDetailsResponse MapToCustomersDetailsResponse(CustomersOrderDetailsVm vm);
    }
}
