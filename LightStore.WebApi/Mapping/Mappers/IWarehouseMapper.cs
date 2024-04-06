using LightStore.Application.Dtos.Warehouse;
using LightStore.WebApi.Dtos.Warehouse;
using Mapster;

namespace LightStore.WebApi.Mapping.Mappers
{
    [Mapper]
    public interface IWarehouseMapper
    {
        WarehouseResponse MapToResponse((WarehouseVm vm, string imageUri) src);
    }
}
