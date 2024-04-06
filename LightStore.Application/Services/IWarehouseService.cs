using LightStore.Application.Dtos.Warehouse;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LightStore.Application.Services
{
    public interface IWarehouseService
    {
        Task<WarehouseVm> Get(Guid id);
        Task<List<WarehouseVm>> GetAll();
        Task<List<WarehouseMinifiedVm>> GetAllMinified();
        Task<Guid> Create(CreateWarehouseDto warehouseDto);
        Task Update(UpdateWarehouseDto warehouseDto);
        Task Delete(Guid id);
    }
}
