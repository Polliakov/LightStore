using LightStore.Application.Dtos.ProductsCategory;
using LightStore.WebApi.Dtos.ProductsCategory;
using Mapster;

namespace LightStore.WebApi.Mapping.Mappers
{
    [Mapper]
    public interface IProductsCategoryMapper
    {
        ProductsCategoryResponse MapToResponse((ProductsCategoryVm vm, string imageUri) src);
    }
}
