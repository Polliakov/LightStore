using System;
using System.Linq.Expressions;
using LightStore.Application.Dtos.Cart;
using LightStore.Application.Mapping.Mappers;
using LightStore.Persistence.Entities;

namespace LightStore.Application.Mapping.Mappers.Implementations
{
    public partial class CartMapper : ICartMapper
    {
        public Expression<Func<ProductInCart, CartItemVm>> ProjectToDtos => p1 => new CartItemVm()
        {
            Id = p1.Product.Id,
            Title = p1.Product.Title,
            Price = p1.Product.Price,
            Count = p1.Count
        };
        public ProductInCart MapFromDto(ValueTuple<Guid, CartItemDto> p2)
        {
            return new ProductInCart()
            {
                ProductId = p2.Item2.ProductId,
                CartId = p2.Item1,
                Count = p2.Item2.Count
            };
        }
    }
}