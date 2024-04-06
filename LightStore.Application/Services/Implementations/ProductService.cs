using LightStore.Application.Dtos.Pagination;
using LightStore.Application.Dtos.Product;
using LightStore.Application.Exceptions;
using LightStore.Application.Filtering.Product;
using LightStore.Application.Mapping.Mappers;
using LightStore.Application.Utils;
using LightStore.Persistence.Entities;
using LightStore.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LightStore.Application.Services.Implementations
{
    public class ProductService : BaseDataService, IProductService
    {
        public ProductService(IProductPriceService priceService,
                              IProductMapper productMapper,
                              ILightStoreDbContext dbContext)
            : base(dbContext)
        {
            this.priceService = priceService;
            this.productMapper = productMapper;
        }

        private readonly IProductPriceService priceService;
        private readonly IProductMapper productMapper;

        public async Task<ProductItemsPageVm> GetItemsPage(
            PaginationArgs pagination,
            ProductFilterArgs filtering)
        {
            var filter = new ProductFilter(dbContext, filtering);

            var query = dbContext.Products.AsNoTracking();
            query = await filter.ApplyAsync(query);
            var foundCount = await query.CountAsync();

            var vms = await query
                .Paginate(pagination)
                .Include(p => p.ActualPrice)
                .Select(productMapper.ProjectToItemDtos)
                .ToListAsync();

            return new ProductItemsPageVm { FoundCount = foundCount, ProductItems = vms };
        }

        public async Task<ProductsPageVm> GetPage(
            PaginationArgs pagination,
            ProductFilterArgs filtering)
        {
            var filter = new ProductFilter(dbContext, filtering);

            var query = dbContext.Products.AsNoTracking();
            query = await filter.ApplyAsync(query);
            var foundCount = await query.CountAsync();

            var vms = await query
                .Paginate(pagination)
                .Include(p => p.ActualPrice)
                .Include(p => p.ProductInWarehouses)
                .Select(p => productMapper
                    .MapToDto(new(p, p.ProductInWarehouses.Count(piw => piw.Count > 0)))
                )
                .ToListAsync();

            return new ProductsPageVm { FoundCount = foundCount, Products = vms };
        }

        public async Task<ProductDetailsVm> GetDetails(Guid id)
        {
            var product = await GetNoTracking(id);
            var availabilityCount = await dbContext.ProductsInWarehouses.AsNoTracking()
                .Where(piw => piw.ProductId == id && piw.Count > 0)
                .CountAsync();
            return productMapper.MapToDetailsDto((product, availabilityCount));
        }

        public async Task<Guid> Create(CreateProductDto productDto)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var product = productMapper.MapFromDto(productDto);
                product.Id = Guid.NewGuid();

                await dbContext.Products.AddAsync(product);
                await dbContext.SaveChangesAsync();

                product.ActualPriceId = await priceService.Create(product.Id, productDto.Price);
                await dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
                return product.Id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task Update(UpdateProductDto dto)
        {
            var oldProduct = await GetNoTracking(dto.Id);

            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var newProduct = productMapper.MapFromDto(dto);
                newProduct.ActualPriceId = dto.Price == oldProduct.Price ?
                    oldProduct.ActualPriceId :
                    await priceService.Create(newProduct.Id, dto.Price);

                dbContext.Products.Update(newProduct);
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task Delete(Guid id)
        {
            var product = await dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new NotFoundException(nameof(Product), id);
            product.Deleted = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
        }

        private async Task<Product> GetNoTracking(Guid id)
        {
            return await dbContext.Products.AsNoTracking()
                .Include(p => p.ActualPrice)
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new NotFoundException(nameof(Product), id);
        }
    }
}
