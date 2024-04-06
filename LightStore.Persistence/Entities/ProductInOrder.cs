using System;

namespace LightStore.Persistence.Entities
{
    public class ProductInOrder
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public Guid ActualPriceId { get; set; }
        public int Count { get; set; }
        public decimal Price => ActualPrice.Value;

        public Product Product { get; set; }
        public Order Order { get; set; }
        public ProductPrice ActualPrice { get; set; }
    }
}
