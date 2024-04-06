using System;

namespace LightStore.Application.Dtos.ProductInAdding
{
    public class ProductInAddingVm
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
    }
}
