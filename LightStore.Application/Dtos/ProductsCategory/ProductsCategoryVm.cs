using System;
using System.Collections.Generic;

namespace LightStore.Application.Dtos.ProductsCategory
{
    public class ProductsCategoryVm
    {
        public Guid Id { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public string Name { get; set; }

        public List<ProductsCategoryVm> ChildСategories { get; set; }
    }
}
