using System;
using System.Linq.Expressions;
using LightStore.Application.Dtos.Warehouse;
using LightStore.Application.Mapping.Mappers;
using LightStore.Persistence.Entities;

namespace LightStore.Application.Mapping.Mappers.Implementations
{
    public partial class WarehouseMapper : IWarehouseMapper
    {
        public Expression<Func<Warehouse, WarehouseVm>> ProjectToDtos => p1 => new WarehouseVm()
        {
            Id = p1.Id,
            Name = p1.Name,
            Address = p1.Address,
            PhoneNumber = p1.PhoneNumber
        };
        public Expression<Func<Warehouse, WarehouseMinifiedVm>> ProjectToMinifiedDtos => p2 => new WarehouseMinifiedVm()
        {
            Id = p2.Id,
            Name = p2.Name
        };
        public WarehouseVm MapToDto(Warehouse p3)
        {
            return p3 == null ? null : new WarehouseVm()
            {
                Id = p3.Id,
                Name = p3.Name,
                Address = p3.Address,
                PhoneNumber = p3.PhoneNumber
            };
        }
        public WarehouseMinifiedVm MapToMinifiedDto(Warehouse p4)
        {
            return p4 == null ? null : new WarehouseMinifiedVm()
            {
                Id = p4.Id,
                Name = p4.Name
            };
        }
        public Warehouse MapFromDto(CreateWarehouseDto p5)
        {
            return p5 == null ? null : new Warehouse()
            {
                Name = p5.Name,
                Address = p5.Address,
                PhoneNumber = p5.PhoneNumber
            };
        }
        public Warehouse MapFromDto(UpdateWarehouseDto p6)
        {
            return p6 == null ? null : new Warehouse()
            {
                Id = p6.Id,
                Name = p6.Name,
                Address = p6.Address,
                PhoneNumber = p6.PhoneNumber
            };
        }
    }
}