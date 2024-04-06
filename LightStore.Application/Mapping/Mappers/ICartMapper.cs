using LightStore.Application.Dtos.Cart;
using LightStore.Persistence.Entities;
using Mapster;
using System;
using System.Linq.Expressions;

namespace LightStore.Application.Mapping.Mappers
{
    [Mapper]
    public interface ICartMapper
    {
        Expression<Func<ProductInCart, CartItemVm>> ProjectToDtos { get; }
        ProductInCart MapFromDto((Guid cartId, CartItemDto item) src);
    }
}
