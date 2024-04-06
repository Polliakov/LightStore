using LightStore.Application.Dtos.ProductInWarehouse;
using LightStore.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LightStore.WebApi.Controllers
{
    public class ProductInWarehouseController : BaseController
    {
        public ProductInWarehouseController(IProductInWarehouseService productInWarehouseService)
        {
            this.productInWarehouseService = productInWarehouseService;
        }

        private readonly IProductInWarehouseService productInWarehouseService;

        [HttpGet]
        public async Task<ActionResult<List<ProductInWarehouseVm>>> GetProductRemains(Guid productId)
        {
            var vms = await productInWarehouseService.GetProductRemains(productId);
            return Ok(vms);
        }

        [HttpPost("MinifiedGeting")]
        public async Task<ActionResult<List<ProductRemainsMinifiedVm>>> GetProductsRemainsMinified(
            [FromBody] List<Guid> productIds)
        {
            var vms = await productInWarehouseService.GetProductsRemainsMinified(productIds);
            return Ok(vms);
        }
    }
}
