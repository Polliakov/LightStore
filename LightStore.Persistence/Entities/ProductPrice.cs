using System;
using System.Collections.Generic;

namespace LightStore.Persistence.Entities
{
    public class ProductPrice
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public decimal Value { get; set; }
        public DateTime StartDate { get; set; }

        public Product Product { get; set; }
        public List<ProductInOrder> ProductsInOrders { get; set; }
    }
}
