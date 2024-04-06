using System;

namespace LightStore.Application.Dtos.ProductInOrder
{
    public class ProductInOrderVm
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
