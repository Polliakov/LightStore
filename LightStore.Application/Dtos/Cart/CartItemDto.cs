using LightStore.Application.Validation.Attributes;
using System;

namespace LightStore.Application.Dtos.Cart
{
    public class CartItemDto
    {
        public Guid ProductId { get; set; }
        [PositiveInteger]
        public int Count { get; set; }
    }
}
