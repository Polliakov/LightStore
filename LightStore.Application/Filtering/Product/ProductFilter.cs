using LightStore.Application.Exceptions;
using LightStore.Application.Utils;
using LightStore.Persistence.Entities;
using LightStore.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities = LightStore.Persistence.Entities;

namespace LightStore.Application.Filtering.Product
{
    public class ProductFilter : IFilter<Entities.Product>
    {
        public ProductFilter(ILightStoreDbContext dbContext, ProductFilterArgs args)
        {
            this.dbContext = dbContext;
            this.args = args;
        }

        private readonly ILightStoreDbContext dbContext;
        private readonly ProductFilterArgs args;

        public async Task<IQueryable<Entities.Product>> ApplyAsync(IQueryable<Entities.Product> query)
        {
            if (!string.IsNullOrWhiteSpace(args.Search))
                query = query.Where(p => p.Title.Contains(args.Search));

            if (args.CategoryId is null)
                return query;

            var categoryIds = await EnumerateCategoryTree(args.CategoryId.Value);
            return query.Where(p => categoryIds.Contains(p.CategoryId.Value));
        }

        private async Task<List<Guid>> EnumerateCategoryTree(Guid rootNodeId)
        {
            var categories = await dbContext.ProductsCategories.ToListAsync();
            var rootNode = categories.FirstOrDefault(c => c.Id == rootNodeId) 
                ?? throw new NotFoundException(nameof(ProductsCategory), rootNodeId);

            return TreeUtils.Enumerate(rootNode, c => c.ChildСategories, c => c.Id);
        }
    }
}
