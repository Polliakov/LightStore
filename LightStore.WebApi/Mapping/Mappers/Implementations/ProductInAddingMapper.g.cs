using System;
using LightStore.Application.Dtos.ProductInAdding;
using LightStore.WebApi.Dtos.ProductInAdding;
using LightStore.WebApi.Mapping.Mappers;

namespace LightStore.WebApi.Mapping.Mappers.Implementations
{
    public partial class ProductInAddingMapper : IProductInAddingMapper
    {
        public ProductInAddingResponse MapToResponse(ValueTuple<ProductInAddingVm, string> p1)
        {
            return new ProductInAddingResponse()
            {
                ImageUri = p1.Item2,
                ProductId = p1.Item1.ProductId,
                Title = p1.Item1.Title,
                Count = p1.Item1.Count
            };
        }
    }
}