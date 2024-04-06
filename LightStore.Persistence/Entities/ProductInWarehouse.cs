using System;

namespace LightStore.Persistence.Entities
{
    public class ProductInWarehouse
    {
        public Guid ProductId { get; set; }
        public Guid WarehouseId { get; set; }
        public int Count { get; set; }

        public Product Product { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
