using LightStore.Persistence.Entities;
using LightStore.Persistence.Interfaces;
using System;
using System.Threading.Tasks;

namespace LightStore.Application.Services.Implementations
{
    public class ProductPriceService : BaseDataService, IProductPriceService
    {
        public ProductPriceService(ILightStoreDbContext dbContext)
            : base(dbContext)
        {
            
        }

        public async Task<Guid> Create(Guid productId, decimal price)
        {
            var productPrice = new ProductPrice
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                Value = price,
                StartDate = DateTime.UtcNow,
            };
            await dbContext.ProductPrices.AddAsync(productPrice);
            await dbContext.SaveChangesAsync();
            return productPrice.Id;
        }
    }
}
