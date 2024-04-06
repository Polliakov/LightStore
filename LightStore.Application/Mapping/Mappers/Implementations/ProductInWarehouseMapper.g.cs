using LightStore.Application.Dtos.ProductInWarehouse;
using LightStore.Application.Dtos.Warehouse;
using LightStore.Application.Mapping.Mappers;
using LightStore.Persistence.Entities;

namespace LightStore.Application.Mapping.Mappers.Implementations
{
    public partial class ProductInWarehouseMapper : IProductInWarehouseMapper
    {
        public ProductInWarehouseVm MapToDto(ProductInWarehouse p1)
        {
            return p1 == null ? null : new ProductInWarehouseVm()
            {
                Count = p1.Count,
                Warehouse = p1.Warehouse == null ? null : new WarehouseVm()
                {
                    Id = p1.Warehouse.Id,
                    Name = p1.Warehouse.Name,
                    Address = p1.Warehouse.Address,
                    PhoneNumber = p1.Warehouse.PhoneNumber
                }
            };
        }
        public ProductInWarehouseMinifiedVm MapToMinifiedDto(ProductInWarehouse p2)
        {
            return p2 == null ? null : new ProductInWarehouseMinifiedVm()
            {
                Count = p2.Count,
                Warehouse = p2.Warehouse == null ? null : new WarehouseMinifiedVm()
                {
                    Id = p2.Warehouse.Id,
                    Name = p2.Warehouse.Name
                }
            };
        }
    }
}