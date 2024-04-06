using System;
using LightStore.Application.Dtos.Product;
using LightStore.WebApi.Dtos.Product;
using LightStore.WebApi.Mapping.Mappers;

namespace LightStore.WebApi.Mapping.Mappers.Implementations
{
    public partial class ProductMapper : IProductMapper
    {
        public ProductDetailsResponse MapToDetailsResponse(ValueTuple<ProductDetailsVm, string> p1)
        {
            return new ProductDetailsResponse()
            {
                ImageUri = p1.Item2,
                CategoryId = p1.Item1.CategoryId,
                Title = p1.Item1.Title,
                AvailabilityCount = p1.Item1.AvailabilityCount,
                Price = p1.Item1.Price,
                Weight = p1.Item1.Weight,
                Size = p1.Item1.Size,
                UnitOfMeasure = p1.Item1.UnitOfMeasure,
                Description = p1.Item1.Description
            };
        }
        public ProductItemResponse MapToItemsResponse(ValueTuple<ProductItemVm, string> p2)
        {
            return new ProductItemResponse()
            {
                ImageUri = p2.Item2,
                Id = p2.Item1.Id,
                Title = p2.Item1.Title,
                Price = p2.Item1.Price,
                UnitOfMeasure = p2.Item1.UnitOfMeasure
            };
        }
        public ProductResponse MapToResponse(ValueTuple<ProductVm, string> p3)
        {
            return new ProductResponse()
            {
                ImageUri = p3.Item2,
                AvailabilityCount = p3.Item1.AvailabilityCount,
                Id = p3.Item1.Id,
                Title = p3.Item1.Title,
                Price = p3.Item1.Price,
                UnitOfMeasure = p3.Item1.UnitOfMeasure
            };
        }
    }
}