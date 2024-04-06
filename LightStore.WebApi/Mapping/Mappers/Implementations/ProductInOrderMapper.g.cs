using System;
using LightStore.Application.Dtos.ProductInOrder;
using LightStore.WebApi.Dtos.ProductInOrder;
using LightStore.WebApi.Mapping.Mappers;

namespace LightStore.WebApi.Mapping.Mappers.Implementations
{
    public partial class ProductInOrderMapper : IProductInOrderMapper
    {
        public ProductInOrderResponse MapToResponse(ValueTuple<ProductInOrderVm, string> p1)
        {
            return new ProductInOrderResponse()
            {
                ImageUri = p1.Item2,
                ProductId = p1.Item1.ProductId,
                Title = p1.Item1.Title,
                Price = p1.Item1.Price,
                Count = p1.Item1.Count
            };
        }
    }
}