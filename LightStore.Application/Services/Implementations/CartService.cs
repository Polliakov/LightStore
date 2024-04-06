using LightStore.Application.Dtos.Cart;
using LightStore.Application.Dtos.ProductInOrder;
using LightStore.Application.Exceptions;
using LightStore.Application.Mapping.Mappers;
using LightStore.Persistence.Entities;
using LightStore.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightStore.Application.Services.Implementations
{
    public class CartService : BaseDataService, ICartService
    {
        public CartService(ICartMapper cartMapper, ILightStoreDbContext dbContext)
            : base(dbContext)
        {
            this.cartMapper = cartMapper;
        }

        private readonly ICartMapper cartMapper;

        public async Task<List<CartItemVm>> GetItems(Guid id)
        {
            var isFound = await IsCartExists(id);
            if (!isFound)
                throw new NotFoundException(nameof(Cart), id);

            var items = await dbContext.ProductsInCarts.AsNoTracking()
                .Where(pic => pic.CartId == id)
                .Include(pic => pic.Product).ThenInclude(p => p.ActualPrice)
                .Select(cartMapper.ProjectToDtos)
                .ToListAsync();

            return items;
        }

        public async Task<Guid> CreateCart()
        {
            var cart = new Cart { Id = Guid.NewGuid() };
            await dbContext.Carts.AddAsync(cart);
            await dbContext.SaveChangesAsync();
            return cart.Id;
        }

        public async Task AddItem(Guid cartId, CartItemDto item)
        {
            await ValidateAdding(cartId, item);

            await dbContext.ProductsInCarts.AddAsync(cartMapper.MapFromDto((cartId, item)));
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateCount(Guid cartId, CartItemDto item)
        {
            var existingItem = await GetExistingItem(cartId, item.ProductId);
            existingItem.Count = item.Count;
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteItem(Guid cartId, Guid itemId)
        {
            var existingItem = await GetExistingItem(cartId, itemId);
            dbContext.ProductsInCarts.Remove(existingItem);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsCartExists(Guid cartId)
        {
            return await dbContext.Carts.AnyAsync(c => c.Id == cartId);
        }

        public async Task<Guid?> GetCartOwnersId(Guid cartId)
        {
            var owner = await dbContext.Customers.AsNoTracking()
                .FirstOrDefaultAsync(c => c.CartId == cartId);
            return owner?.AppUserId;
        }

        private async Task<ProductInCart> GetExistingItem(Guid cartId, Guid itemId)
        {
            var existingItem = await dbContext.ProductsInCarts
                .FirstOrDefaultAsync(pic => pic.CartId == cartId && pic.ProductId == itemId)
                ?? throw new NotFoundException(nameof(Product), $"cart: {itemId}, product: {itemId}");
            return existingItem;
        }

        public async Task RemoveOrderedItems(
            IEnumerable<CreateProductInOrderDto> orderItems, Guid cartOwnersId)
        {
            var cartOwner = await dbContext.Customers.AsNoTracking()
                .FirstOrDefaultAsync(c => c.AppUserId == cartOwnersId)
                ?? throw new NotFoundException(nameof(Customer), cartOwnersId);

            var cartItems = await dbContext.ProductsInCarts
                .Where(pic => pic.CartId == cartOwner.CartId)
                .ToListAsync();

            DecreaseOrRemove(cartItems, orderItems);

            await dbContext.SaveChangesAsync();
        }

        private void DecreaseOrRemove(
            IEnumerable<ProductInCart> cartItems, IEnumerable<CreateProductInOrderDto> orderItems)
        {
            var join = cartItems.Join(
                orderItems,
                ci => ci.ProductId,
                oi => oi.ProductId,
                (ci, oi) => (cartItem: ci, orderItem: oi)
            );

            foreach (var (cartItem, orderItem) in join)
            {
                cartItem.Count -= orderItem.Count;
                if (cartItem.Count <= 0)
                    dbContext.ProductsInCarts.Remove(cartItem);
            }
        }

        private async Task ValidateAdding(Guid cartId, CartItemDto item)
        {
            var cartExists = await IsCartExists(cartId);
            if (!cartExists)
                throw new NotFoundException(nameof(Cart), cartId);

            var productExists = await dbContext.Products.AnyAsync(p => p.Id == item.ProductId);
            if (!productExists)
                throw new NotFoundException(nameof(Product), item.ProductId);

            var itemExists = await dbContext.ProductsInCarts
                .AnyAsync(pic => pic.CartId == cartId && pic.ProductId == item.ProductId);
            if (itemExists)
                throw new ExistsException(nameof(ProductInCart), (cartId, item.ProductId));
        }
    }
}
