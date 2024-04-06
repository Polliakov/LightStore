using LightStore.Application.Dtos.ProductInOrder;
using LightStore.WebApi.Dtos.ProductInOrder;
using Mapster;

namespace LightStore.WebApi.Mapping.Mappers
{
    [Mapper]
    public interface IProductInOrderMapper
    {
        ProductInOrderResponse MapToResponse((ProductInOrderVm, string imageUri) src);
    }
}
