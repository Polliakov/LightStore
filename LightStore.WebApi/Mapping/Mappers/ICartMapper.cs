using LightStore.Application.Dtos.Cart;
using LightStore.WebApi.Dtos.Cart;
using Mapster;

namespace LightStore.WebApi.Mapping.Mappers
{
    [Mapper]
    public interface ICartMapper
    {
        CartItemResponse MapToResponse((CartItemVm cartItemVm, string imageUri) src);
    }
}
