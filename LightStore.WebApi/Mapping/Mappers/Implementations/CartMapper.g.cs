using System;
using LightStore.Application.Dtos.Cart;
using LightStore.WebApi.Dtos.Cart;
using LightStore.WebApi.Mapping.Mappers;

namespace LightStore.WebApi.Mapping.Mappers.Implementations
{
    public partial class CartMapper : ICartMapper
    {
        public CartItemResponse MapToResponse(ValueTuple<CartItemVm, string> p1)
        {
            return new CartItemResponse()
            {
                ImageUri = p1.Item2,
                Id = p1.Item1.Id,
                Title = p1.Item1.Title,
                Price = p1.Item1.Price,
                Count = p1.Item1.Count
            };
        }
    }
}