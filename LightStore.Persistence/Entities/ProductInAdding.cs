using System;

namespace LightStore.Persistence.Entities
{
    public class ProductInAdding
    {
        public Guid ProductId { get; set; }
        public Guid ProductsAddingId { get; set; }
        public int Count { get; set; }

        public Product Product { get; set; }
        public ProductsAdding ProductsAdding { get; set; }
    }
}
