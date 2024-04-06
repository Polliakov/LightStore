using LightStore.Application.Dtos.ProductsCategory;
using LightStore.Application.Exceptions;
using LightStore.Application.Mapping.Mappers;
using LightStore.Application.Utils;
using LightStore.Persistence.Entities;
using LightStore.Persistence.Interfaces;
using LightStore.Persistence.RawSql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightStore.Application.Services.Implementations
{
    public class ProductsCategoryService : BaseDataService, IProductsCategoryService
    {
        public ProductsCategoryService(IProductsCategoryMapper productsCategoryMapper,
                                       IRawSqlProvider rawSqlProvider,
                                       ILightStoreDbContext dbContext)
            : base(dbContext)
        {
            this.rawSqlProvider = rawSqlProvider;
            this.productsCategoryMapper = productsCategoryMapper;
        }

        private readonly IRawSqlProvider rawSqlProvider;
        private readonly IProductsCategoryMapper productsCategoryMapper;

        public async Task<ProductsCategoryVm> Get(Guid id)
        {
            var category = await dbContext.ProductsCategories.AsNoTracking()
                .Include(c => c.ChildСategories)
                .FirstOrDefaultAsync(c => c.Id == id)
                ?? throw new NotFoundException(nameof(ProductsCategory), id);

            if (category.ChildСategories.Count == 0)
                category.ChildСategories = null;

            return productsCategoryMapper.MapToDto(category);
        }

        public async Task<List<ProductsCategoryVm>> GetAll()
        {
            var categories = await dbContext.ProductsCategories.ToListAsync();
            var rootCategories = categories
                .Where(c => c.ParentCategoryId is null)
                .Select(c => productsCategoryMapper.MapToDto(c))
                .ToList();

            return rootCategories;
        }

        public async Task<Guid> Create(CreateProductsCategoryDto dto)
        {
            var category = productsCategoryMapper.MapFromDto(dto);
            category.Id = Guid.NewGuid();

            await dbContext.ProductsCategories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category.Id;
        }

        public async Task Update(UpdateProductsCategoryDto dto)
        {
            if (dto.Id == dto.ParentCategoryId)
                throw new InvalidOperationException("Can't set category as parent for itself.");

            var isFound = await dbContext.ProductsCategories.AnyAsync(c => c.Id == dto.Id);
            if (!isFound)
                throw new NotFoundException(nameof(ProductsCategory), dto.Id);

            if (dto.ParentCategoryId.HasValue)
                await UpdateParentCategory(dto.Id, dto.ParentCategoryId.Value);

            var category = productsCategoryMapper.MapFromDto(dto);
            dbContext.ProductsCategories.Update(category);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var category = await dbContext.ProductsCategories
                .Include(c => c.ChildСategories)
                .FirstOrDefaultAsync(c => c.Id == id)
                ?? throw new NotFoundException(nameof(ProductsCategory), id);

            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                await dbContext.Database.ExecuteSqlRawAsync(
                    rawSqlProvider.MoveProductsToCategory(category.Id, category.ParentCategoryId)
                );

                if (category.ChildСategories is not null && category.ChildСategories.Count > 0)
                {
                    foreach (var childCategory in category.ChildСategories)
                        childCategory.ParentCategoryId = category.ParentCategoryId;
                }
                dbContext.ProductsCategories.Remove(category);

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task UpdateParentCategory(Guid Id, Guid parentCategoryId)
        {
            var categories = await dbContext.ProductsCategories.ToListAsync();

            var parentCategory = categories.FirstOrDefault(c => c.Id == parentCategoryId)
                ?? throw new NotFoundException(nameof(ProductsCategory), parentCategoryId);

            var category = categories.First(c => c.Id == Id);

            if (IsCategoryDowncast(Id, parentCategory))
                category.ChildСategories.ForEach(c => c.ParentCategoryId = category.ParentCategoryId);

            dbContext.Detach(category);
        }

        private static bool IsCategoryDowncast(Guid Id, ProductsCategory newParent)
        {
            return TreeUtils.AnyParent(newParent, c => c.ParentCategory, c => c.Id == Id);
        }
    }
}
