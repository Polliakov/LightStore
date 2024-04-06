using System;

namespace LightStore.Persistence.Entities
{
    public class ProductInCart
    {
        public Guid ProductId { get; set; }
        public Guid CartId { get; set; }
        public int Count { get; set; }

        public Product Product { get; set; }
        public Cart Cart { get; set; }
    }
}
