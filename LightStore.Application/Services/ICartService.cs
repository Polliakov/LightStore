using LightStore.Application.Dtos.Cart;
using LightStore.Application.Dtos.ProductInOrder;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LightStore.Application.Services
{
    public interface ICartService
    {
        Task<List<CartItemVm>> GetItems(Guid id);
        Task<Guid> CreateCart();
        Task AddItem(Guid cartId, CartItemDto item);
        Task UpdateCount(Guid cartId, CartItemDto item);
        Task DeleteItem(Guid cartId, Guid itemId);
        Task<bool> IsCartExists(Guid cartId);
        Task<Guid?> GetCartOwnersId(Guid cartId);
        Task RemoveOrderedItems(IEnumerable<CreateProductInOrderDto> orderItems, Guid cartOwnersId);
    }
}
