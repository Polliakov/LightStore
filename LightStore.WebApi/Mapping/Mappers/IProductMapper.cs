using LightStore.Application.Dtos.Product;
using LightStore.WebApi.Dtos.Product;
using Mapster;

namespace LightStore.WebApi.Mapping.Mappers
{
    [Mapper]
    public interface IProductMapper
    {
        ProductDetailsResponse MapToDetailsResponse((ProductDetailsVm, string imageUri) src);
        ProductItemResponse MapToItemsResponse((ProductItemVm, string imageUri) src);
        ProductResponse MapToResponse((ProductVm, string imageUri) src);
    }
}
