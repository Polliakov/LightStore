using System;
using LightStore.Application.Dtos.Warehouse;
using LightStore.WebApi.Dtos.Warehouse;
using LightStore.WebApi.Mapping.Mappers;

namespace LightStore.WebApi.Mapping.Mappers.Implementations
{
    public partial class WarehouseMapper : IWarehouseMapper
    {
        public WarehouseResponse MapToResponse(ValueTuple<WarehouseVm, string> p1)
        {
            return new WarehouseResponse()
            {
                ImageUri = p1.Item2,
                Id = p1.Item1.Id,
                Name = p1.Item1.Name,
                Address = p1.Item1.Address,
                PhoneNumber = p1.Item1.PhoneNumber
            };
        }
    }
}