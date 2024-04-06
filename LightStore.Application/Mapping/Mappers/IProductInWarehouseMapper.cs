using LightStore.Application.Dtos.ProductInWarehouse;
using LightStore.Persistence.Entities;
using Mapster;

namespace LightStore.Application.Mapping.Mappers
{
    [Mapper]
    public interface IProductInWarehouseMapper
    {
        ProductInWarehouseVm MapToDto(ProductInWarehouse productInWarehouse);
        ProductInWarehouseMinifiedVm MapToMinifiedDto(ProductInWarehouse productInWarehouse);
    }
}
