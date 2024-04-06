using System.Collections.Generic;
using LightStore.Application.Dtos.ProductsCategory;
using LightStore.Application.Mapping.Mappers;
using LightStore.Persistence.Entities;
using Mapster;

namespace LightStore.Application.Mapping.Mappers.Implementations
{
    public partial class ProductsCategoryMapper : IProductsCategoryMapper
    {
        public ProductsCategoryVm MapToDto(ProductsCategory p1)
        {
            return p1 == null ? null : new ProductsCategoryVm()
            {
                Id = p1.Id,
                ParentCategoryId = p1.ParentCategoryId,
                Name = p1.Name,
                ChildСategories = funcMain1(p1.ChildСategories)
            };
        }
        public ProductsCategory MapFromDto(CreateProductsCategoryDto p3)
        {
            return p3 == null ? null : new ProductsCategory()
            {
                ParentCategoryId = p3.ParentCategoryId,
                Name = p3.Name
            };
        }
        public ProductsCategory MapFromDto(UpdateProductsCategoryDto p4)
        {
            return p4 == null ? null : new ProductsCategory()
            {
                Id = p4.Id,
                ParentCategoryId = p4.ParentCategoryId,
                Name = p4.Name
            };
        }
        
        private List<ProductsCategoryVm> funcMain1(List<ProductsCategory> p2)
        {
            if (p2 == null)
            {
                return null;
            }
            List<ProductsCategoryVm> result = new List<ProductsCategoryVm>(p2.Count);
            
            int i = 0;
            int len = p2.Count;
            
            while (i < len)
            {
                ProductsCategory item = p2[i];
                result.Add(TypeAdapter<ProductsCategory, ProductsCategoryVm>.Map.Invoke(item));
                i++;
            }
            return result;
            
        }
    }
}