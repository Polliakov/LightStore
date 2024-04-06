using LightStore.Application.Dtos.Warehouse;
using LightStore.Persistence.Entities;
using Mapster;
using System;
using System.Linq.Expressions;

namespace LightStore.Application.Mapping.Mappers
{
    [Mapper]
    public interface IWarehouseMapper
    {
        WarehouseVm MapToDto(Warehouse warehouse);
        WarehouseMinifiedVm MapToMinifiedDto(Warehouse warehouse);
        Expression<Func<Warehouse, WarehouseVm>> ProjectToDtos { get; }
        Expression<Func<Warehouse, WarehouseMinifiedVm>> ProjectToMinifiedDtos { get; }
        Warehouse MapFromDto(CreateWarehouseDto warehouseDto);
        Warehouse MapFromDto(UpdateWarehouseDto warehouseDto);
    }
}
