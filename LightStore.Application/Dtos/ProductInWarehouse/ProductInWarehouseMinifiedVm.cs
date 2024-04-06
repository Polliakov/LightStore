using LightStore.Application.Dtos.Warehouse;

namespace LightStore.Application.Dtos.ProductInWarehouse
{
    public class ProductInWarehouseMinifiedVm
    {
        public int Count { get; set; }
        public WarehouseMinifiedVm Warehouse { get; set; }
    }
}
