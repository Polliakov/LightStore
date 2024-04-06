using LightStore.Application.Dtos.Product;
using System.Collections.Generic;

namespace LightStore.WebApi.Dtos.Product
{
    public class ProductsPageResponse : ProductsPageVm
    {
        public new IEnumerable<ProductResponse> Products { get; set; }
    }
}
