using LightStore.Application.Dtos.ProductInOrder;
using LightStore.Persistence.Entities;
using LightStore.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightStore.Application.Services.Implementations
{
    public class ProductInOrderService : BaseDataService, IProductInOrderService
    {
        public ProductInOrderService(ILightStoreDbContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<List<ProductInOrder>> LinkProductsInOrder(
            Guid orderId,
            IEnumerable<ProductInOrder> productsInOrder)
        {
            var productIds = productsInOrder.Select(pio => pio.ProductId);
            var productPrices = await dbContext.Products
                .Where(p => productIds.Contains(p.Id))
                .Select(p => new { p.Id, p.ActualPriceId })
                .ToListAsync();

            return productsInOrder
                .Join(
                    productPrices,
                    productInOrder => productInOrder.ProductId,
                    productPrice => productPrice.Id,
                    (productInOrder, productAndPrice) =>
                    {
                        productInOrder.ActualPriceId = productAndPrice.ActualPriceId.GetValueOrDefault();
                        productInOrder.OrderId = orderId;
                        return productInOrder;
                    }
                ).ToList();
        }
    }
}
