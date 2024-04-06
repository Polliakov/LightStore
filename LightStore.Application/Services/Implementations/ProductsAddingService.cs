using LightStore.Application.Dtos.ProductsAdding;
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
    public class ProductsAddingService : BaseDataService, IProductsAddingService
    {
        public ProductsAddingService(IProductInWarehouseService productInWarehouseService,
                                     IProductsAddingMapper productsAddingMapper,
                                     ILightStoreDbContext dbContext)
            : base(dbContext)
        {
            this.productInWarehouseService = productInWarehouseService;
            this.productsAddingMapper = productsAddingMapper;
        }

        private readonly IProductInWarehouseService productInWarehouseService;
        private readonly IProductsAddingMapper productsAddingMapper;

        public async Task<ProductsAddingDetailsVm> GetDetails(Guid id)
        {
            var adding = await dbContext.ProductsAddings.AsNoTracking().IgnoreQueryFilters()
                .Include(pa => pa.Employee)
                .Include(pa => pa.Warehouse)
                .Include(pa => pa.ProductsInAdding).ThenInclude(pia => pia.Product)
                .FirstOrDefaultAsync(pa => pa.Id == id);

            if (adding is null)
                throw new NotFoundException(nameof(ProductsAdding), id);

            return productsAddingMapper.MapToDetailsDto(adding);
        }

        public async Task<List<ProductsAddingVm>> GetAll()
        {
            return await dbContext.ProductsAddings.AsNoTracking()
                .Include(pa => pa.Employee)
                .Include(pa => pa.Warehouse)
                .OrderByDescending(pa => pa.CreationDate)
                .Select(productsAddingMapper.ProjectToDtos)
                .ToListAsync();
        }

        public async Task<Guid> Create(CreateProductsAddingDto dto, Guid emloyeeId)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                await productInWarehouseService.AddCount(dto.ProductsInAdding, dto.WarehouseId);

                var adding = productsAddingMapper.MapFromDto(dto);
                adding.Id = Guid.NewGuid();
                adding.EmployeeId = emloyeeId;
                adding.CreationDate = DateTime.UtcNow;
                for (var i = 0; i < adding.ProductsInAdding.Count; i++)
                    adding.ProductsInAdding[i].ProductsAddingId = adding.Id;

                await dbContext.ProductsAddings.AddAsync(adding);
                await dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
                return adding.Id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
