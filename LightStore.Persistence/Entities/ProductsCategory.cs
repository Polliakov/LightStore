using System;
using System.Collections.Generic;

namespace LightStore.Persistence.Entities
{
    public class ProductsCategory
    {
        public Guid Id { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public string Name { get; set; }

        public ProductsCategory ParentCategory { get; set; }
        public List<ProductsCategory> ChildСategories { get; set; }
        public List<Product> Products { get; set; }
    }
}
