using System;
using System.Collections.Generic;

namespace LightStore.Persistence.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }

        public Customer Customer { get; set; }
        public List<ProductInCart> Items { get; set; }
    }
}
