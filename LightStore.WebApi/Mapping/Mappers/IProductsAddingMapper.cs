using LightStore.Application.Dtos.ProductsAdding;
using LightStore.WebApi.Dtos.ProductsAdding;
using Mapster;

namespace LightStore.WebApi.Mapping.Mappers
{
    [Mapper]
    public interface IProductsAddingMapper
    {
        ProductsAddingDetailsResponse MapToDetailsResponse(ProductsAddingDetailsVm vm);
    }
}
