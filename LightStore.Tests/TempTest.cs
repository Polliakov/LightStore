using LightStore.Persistence.Entities;
using LightStore.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LightStore.Tests
{
    public class TempTest
    {
        [Fact]
        public void ViewSqlAsync()
        {
            var dbContext = LightStoreDbContextFactory.Create();
            var order = new Order
            {
                ProductsInOrder = new List<ProductInOrder> { new ProductInOrder { ProductId = Guid.NewGuid() } }
            };

            var queryString = (dbContext.Products
                .Include(p => p.ActualPrice)
                .GroupJoin(
                    dbContext.AppUsers,
                    p => p.Id,
                    pio => pio.AppUserId,
                    (product, productInOrder) => new { product, productInOrder }
                ) as IQueryable).ToQueryString();

            Debug.WriteLine(queryString);
            Assert.True(true);
        }
    }
}
