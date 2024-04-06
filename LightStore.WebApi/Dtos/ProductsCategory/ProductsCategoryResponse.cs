using LightStore.Application.Dtos.ProductsCategory;
using System.Collections.Generic;

namespace LightStore.WebApi.Dtos.ProductsCategory
{
    public class ProductsCategoryResponse : ProductsCategoryVm
    {
        public string ImageUri { get; set; }

        public new List<ProductsCategoryResponse> ChildСategories { get; set; }
    }
}
