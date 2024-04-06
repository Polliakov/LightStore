using System;
using LightStore.Application.Dtos.ProductsCategory;
using LightStore.WebApi.Dtos.ProductsCategory;
using LightStore.WebApi.Mapping.Mappers;

namespace LightStore.WebApi.Mapping.Mappers.Implementations
{
    public partial class ProductsCategoryMapper : IProductsCategoryMapper
    {
        public ProductsCategoryResponse MapToResponse(ValueTuple<ProductsCategoryVm, string> p1)
        {
            return new ProductsCategoryResponse()
            {
                ImageUri = p1.Item2,
                Id = p1.Item1.Id,
                ParentCategoryId = p1.Item1.ParentCategoryId,
                Name = p1.Item1.Name
            };
        }
    }
}