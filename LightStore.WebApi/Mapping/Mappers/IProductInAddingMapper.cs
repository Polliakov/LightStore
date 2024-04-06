using LightStore.Application.Dtos.ProductInAdding;
using LightStore.WebApi.Dtos.ProductInAdding;
using Mapster;

namespace LightStore.WebApi.Mapping.Mappers
{
    [Mapper]
    public interface IProductInAddingMapper
    {
        ProductInAddingResponse MapToResponse((ProductInAddingVm vm, string imageUri) src);
    }
}
