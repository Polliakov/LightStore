using LightStore.Application.Dtos.Cart;
using LightStore.Application.Services;
using LightStore.ImageService.Interfaces;
using LightStore.WebApi.Dtos.Cart;
using LightStore.WebApi.Mapping.Mappers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightStore.WebApi.Controllers
{
    public class CartController : BaseController
    {
        public CartController(ICartService cartService,
                              ICartMapper cartMapper,
                              IImageService imageService)
        {
            this.cartService = cartService;
            this.cartMapper = cartMapper;
            this.imageService = imageService;
        }

        private readonly ICartService cartService;
        private readonly ICartMapper cartMapper;
        private readonly IImageService imageService;

        [HttpGet("{id}")]
        public async Task<ActionResult<List<CartItemResponse>>> GetItems(Guid id)
        {
            var vms = await cartService.GetItems(id);
            var response = vms.Select(vm =>
                cartMapper.MapToResponse((vm, imageService.GetImageUri(vm.Id)))
            );
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Guid>> CreateCart()
        {
            var id = await cartService.CreateCart();
            return Ok(id);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddItem(Guid id, [FromBody] CartItemDto item)
        {
            await cartService.AddItem(id, item);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCount(Guid id, [FromBody] CartItemDto item)
        {
            await cartService.UpdateCount(id, item);
            return NoContent();
        }

        [HttpDelete("{cartId}/Item/{itemId}")]
        public async Task<IActionResult> DeleteItem(Guid cartId, Guid itemId)
        {
            await cartService.DeleteItem(cartId, itemId);
            return NoContent();
        }
    }
}
