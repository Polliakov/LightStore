using LightStore.Application.Dtos.Warehouse;
using LightStore.Application.Exceptions;
using LightStore.Application.Mapping.Mappers;
using LightStore.Persistence.Entities;
using LightStore.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightStore.Application.Services.Implementations
{
    public class WarehouseService : BaseDataService, IWarehouseService
    {
        public WarehouseService(ILightStoreDbContext dbContext, IWarehouseMapper warehouseMapper)
            : base(dbContext)
        {
            this.warehouseMapper = warehouseMapper;
        }

        private readonly IWarehouseMapper warehouseMapper;

        public async Task<WarehouseVm> Get(Guid id)
        {
            var warehouse = await dbContext.Warehouses.AsNoTracking()
                .FirstOrDefaultAsync(w => w.Id == id)
                ?? throw new NotFoundException(nameof(Warehouse), id);
            return warehouseMapper.MapToDto(warehouse);
        }

        public async Task<List<WarehouseVm>> GetAll()
        {
            return await dbContext.Warehouses.AsNoTracking()
                .Select(warehouseMapper.ProjectToDtos).ToListAsync();
        }

        public async Task<List<WarehouseMinifiedVm>> GetAllMinified()
        {
            return await dbContext.Warehouses.AsNoTracking()
                .Select(warehouseMapper.ProjectToMinifiedDtos).ToListAsync();
        }

        public async Task<Guid> Create(CreateWarehouseDto warehouseDto)
        {
            var warehouse = warehouseMapper.MapFromDto(warehouseDto);
            warehouse.Id = Guid.NewGuid();
            await dbContext.Warehouses.AddAsync(warehouse);
            await dbContext.SaveChangesAsync();
            return warehouse.Id;
        }

        public async Task Update(UpdateWarehouseDto warehouseDto)
        {
            var isFound = await dbContext.Warehouses.AnyAsync(w => w.Id == warehouseDto.Id);
            if (!isFound)
                throw new NotFoundException(nameof(Warehouse), warehouseDto.Id);

            var warehouse = warehouseMapper.MapFromDto(warehouseDto);
            dbContext.Warehouses.Update(warehouse);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var warehouse = await dbContext.Warehouses.FirstOrDefaultAsync(w => w.Id == id)
                ?? throw new NotFoundException(nameof(Warehouse), id);

            warehouse.Deleted = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
        }
    }
}
