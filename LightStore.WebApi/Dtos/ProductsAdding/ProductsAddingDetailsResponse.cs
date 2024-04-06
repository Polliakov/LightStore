using LightStore.Application.Dtos.ProductsAdding;
using LightStore.WebApi.Dtos.ProductInAdding;
using System.Collections.Generic;

namespace LightStore.WebApi.Dtos.ProductsAdding
{
    public class ProductsAddingDetailsResponse : ProductsAddingDetailsVm
    {
        public new IEnumerable<ProductInAddingResponse> ProductsInAdding { get; set; }
    }
}
