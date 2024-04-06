using LightStore.Application.Dtos.ProductInWarehouse;
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
    public class ProductInWarehouseService : BaseDataService, IProductInWarehouseService
    {
        public ProductInWarehouseService(IProductInWarehouseMapper productInWarehouseMapper,
                                         ILightStoreDbContext dbContext)
            : base(dbContext)
        {
            this.productInWarehouseMapper = productInWarehouseMapper;
        }

        private readonly IProductInWarehouseMapper productInWarehouseMapper;

        public async Task<List<ProductInWarehouseVm>> GetProductRemains(Guid productId)
        {
            var isFound = await dbContext.Products.AnyAsync(p => p.Id == productId);
            if (!isFound)
                throw new NotFoundException(nameof(Product), productId);

            return await dbContext.ProductsInWarehouses.AsNoTracking()
                .Where(piw => piw.ProductId == productId && piw.Count > 0)
                .Include(piw => piw.Warehouse)
                .Select(piw => productInWarehouseMapper.MapToDto(piw))
                .ToListAsync();
        }

        public async Task<List<ProductRemainsMinifiedVm>> GetProductsRemainsMinified(IEnumerable<Guid> productIds)
        {
            await ValidateProductIds(productIds);

            var piws = await dbContext.ProductsInWarehouses.AsNoTracking()
                .Where(piw => productIds.Contains(piw.ProductId))
                .Include(piw => piw.Warehouse)
                .ToListAsync();

            return piws
                .GroupBy(piw => piw.ProductId)
                .Select(group => new ProductRemainsMinifiedVm
                {
                    ProductId = group.Key,
                    Remains = group
                        .Select(piw => productInWarehouseMapper.MapToMinifiedDto(piw))
                        .ToList()
                })
                .ToList();
        }

        public async Task AddCount(
            IEnumerable<IChangeProductCountDto> changeDtos, 
            Guid warehouseId)
        {
            var changeProductsJoin = await GetChangeProductsJoin(changeDtos, warehouseId);
            await IncreaseCountOrMakeNew(changeProductsJoin, warehouseId);

            await dbContext.SaveChangesAsync();
        }

        public async Task Writeoff(
            IEnumerable<IChangeProductCountDto> changeDtos,
            Guid warehouseId)
        {
            var changeProductsJoin = await GetChangeProductsJoin(changeDtos, warehouseId);
            DecreaseCountOrThrow(changeProductsJoin, warehouseId);

            await dbContext.SaveChangesAsync();
        }

        private async Task IncreaseCountOrMakeNew(
            IEnumerable<(IChangeProductCountDto dto, ProductInWarehouse piw)> changeProductsJoin,
            Guid warehouseId)
        {
            var newProducts = changeProductsJoin.Where(join => join.piw is null)
                            .Select(join => CreateProductInWarehouseInstance(join.dto, warehouseId));
            await dbContext.ProductsInWarehouses.AddRangeAsync(newProducts);

            foreach (var (dto, piw) in changeProductsJoin.Where(join => join.piw is not null))
                piw.Count += dto.Count;
        }

        private static void DecreaseCountOrThrow(
            IEnumerable<(IChangeProductCountDto dto, ProductInWarehouse piw)> changeProductsJoin,
            Guid warehouseId)
        {
            var dtoWithNoProduct = changeProductsJoin.FirstOrDefault(join => join.piw is null).dto;
            if (dtoWithNoProduct is not null)
                throw new WarehouseWriteoffException(warehouseId, 0, dtoWithNoProduct);

            foreach (var (dto, piw) in changeProductsJoin)
            {
                piw.Count -= dto.Count;
                if (piw.Count < 0)
                    throw new WarehouseWriteoffException(warehouseId, piw.Count + dto.Count, dto);
            }
        }

        private async Task<IEnumerable<(IChangeProductCountDto dto, ProductInWarehouse piw)>> GetChangeProductsJoin(
            IEnumerable<IChangeProductCountDto> changeDtos, 
            Guid warehouseId)
        {
            await ValidateParams(changeDtos, warehouseId);
            var productIds = await GetValidatedProductIds(changeDtos);

            var productsInWarehouse = await dbContext.ProductsInWarehouses
                .Where(piw => piw.WarehouseId == warehouseId && productIds.Contains(piw.ProductId))
                .ToListAsync();

            return LeftJoin(changeDtos, productsInWarehouse);
        }

        private async Task ValidateParams(
            IEnumerable<IChangeProductCountDto> changeDtos, 
            Guid warehouseId)
        {
            if (changeDtos is null)
                throw new ArgumentNullException(nameof(changeDtos));

            if (changeDtos.Any(dto => dto.Count < 0))
                throw new ArgumentOutOfRangeException(nameof(changeDtos), "Any dto has count < 0.");

            var warehouseIsFound = await dbContext.Warehouses.AnyAsync(w => w.Id == warehouseId);
            if (!warehouseIsFound)
                throw new NotFoundException(nameof(Warehouse), warehouseId);
        }

        private async Task<List<Guid>> GetValidatedProductIds(IEnumerable<IChangeProductCountDto> changeDtos)
        {
            var productIds = changeDtos.Select(dto => dto.ProductId).ToList();
            await ValidateProductIds(productIds);
            return productIds;
        }

        private async Task ValidateProductIds(IEnumerable<Guid> productIds)
        {
            if (productIds.Distinct().Count() != productIds.Count())
                throw new DuplicateException(nameof(productIds));

            var foundProductsCount = await dbContext.Products
                .Where(p => productIds.Contains(p.Id))
                .CountAsync();

            if (foundProductsCount != productIds.Count())
                throw new NotFoundException(nameof(Product));
        }

        private static IEnumerable<(IChangeProductCountDto dto, ProductInWarehouse piw)> LeftJoin(
            IEnumerable<IChangeProductCountDto> changeDtos,
            IEnumerable<ProductInWarehouse> productsInWarehouse)
        {
            return changeDtos
                .GroupJoin(productsInWarehouse, dto => dto.ProductId, piw => piw.ProductId, (dto, piw) => (dto, piw))
                .SelectMany(group => group.piw.DefaultIfEmpty(), (group, piw) => (group.dto, piw));
        }

        private static ProductInWarehouse CreateProductInWarehouseInstance(IChangeProductCountDto dto, Guid warehouseId)
        {
            return new ProductInWarehouse
            {
                WarehouseId = warehouseId,
                ProductId = dto.ProductId,
                Count = dto.Count,
            };
        }
    }
}
