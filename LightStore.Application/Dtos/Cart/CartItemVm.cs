using System;

namespace LightStore.Application.Dtos.Cart
{
    public class CartItemVm
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
