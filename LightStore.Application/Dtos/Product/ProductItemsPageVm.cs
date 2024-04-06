using System.Collections.Generic;

namespace LightStore.Application.Dtos.Product
{
    public class ProductItemsPageVm
    {
        public int FoundCount { get; set; }
        public List<ProductItemVm> ProductItems { get; set; }
    }
}
