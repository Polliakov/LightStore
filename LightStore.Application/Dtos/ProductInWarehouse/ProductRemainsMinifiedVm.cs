using System;
using System.Collections.Generic;

namespace LightStore.Application.Dtos.ProductInWarehouse
{
    public class ProductRemainsMinifiedVm
    {
        public Guid ProductId { get; set; }
        public List<ProductInWarehouseMinifiedVm> Remains { get; set; }
    }
}
