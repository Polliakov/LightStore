using LightStore.Application.Dtos.ProductsCategory;
using LightStore.Persistence.Entities;
using Mapster;

namespace LightStore.Application.Mapping.Mappers
{
    [Mapper]
    public interface IProductsCategoryMapper
    {
        ProductsCategoryVm MapToDto(ProductsCategory entity);
        ProductsCategory MapFromDto(CreateProductsCategoryDto dto);
        ProductsCategory MapFromDto(UpdateProductsCategoryDto dto);
    }
}
