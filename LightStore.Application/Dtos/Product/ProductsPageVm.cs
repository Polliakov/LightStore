using System.Collections.Generic;

namespace LightStore.Application.Dtos.Product
{
    public class ProductsPageVm
    {
        public int FoundCount { get; set; }
        public List<ProductVm> Products { get; set; }
    }
}
