using LightStore.Application.Dtos.Warehouse;

namespace LightStore.Application.Dtos.ProductInWarehouse
{
    public class ProductInWarehouseVm
    {
        public int Count { get; set; }
        public WarehouseVm Warehouse { get; set; }
    }
}
