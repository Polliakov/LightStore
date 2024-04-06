using LightStore.Application.Dtos.Product;
using System.Collections.Generic;

namespace LightStore.WebApi.Dtos.Product
{
    public class ProductItemsPageResponse : ProductItemsPageVm
    {
        public new IEnumerable<ProductItemResponse> ProductItems { get; set; }
    }
}
