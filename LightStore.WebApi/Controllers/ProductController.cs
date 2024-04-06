using LightStore.Application.Dtos.Pagination;
using LightStore.Application.Dtos.Product;
using LightStore.Application.Filtering.Product;
using LightStore.Application.Services;
using LightStore.ImageService;
using LightStore.ImageService.Interfaces;
using LightStore.Persistence.Entities.Base;
using LightStore.WebApi.Attributes;
using LightStore.WebApi.Binding;
using LightStore.WebApi.Dtos.Product;
using LightStore.WebApi.Mapping.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LightStore.WebApi.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(IProductService productService,
                                 IProductMapper productMapper,
                                 IImageService imageService)
        {
            this.productService = productService;
            this.productMapper = productMapper;
            this.imageService = imageService;
        }

        private readonly IProductService productService;
        private readonly IProductMapper productMapper;
        private readonly IImageService imageService;

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailsResponse>> GetDetails(Guid id)
        {
            var vm = await productService.GetDetails(id);
            var imageUri = imageService.GetImageUri(id);
            return Ok(productMapper.MapToDetailsResponse((vm, imageUri)));
        }

        [HttpGet("/ProductsPage")]
        public async Task<ActionResult<ProductsPageResponse>> GetPage(
            int page, int pageSize,
            string search, Guid? categoryId)
        {
            var productsPage = await productService.GetPage(
                new PaginationArgs(page, pageSize),
                new ProductFilterArgs(search, categoryId)
            );
            var products = productsPage.Products.Select(pv =>
                productMapper.MapToResponse((pv, imageService.GetImageUri(pv.Id)))
            );
            var response = new ProductsPageResponse
            {
                FoundCount = productsPage.FoundCount,
                Products = products
            };
            return Ok(response);
        }

        [HttpGet("/ProductItemsPage")]
        public async Task<ActionResult<ProductItemsPageVm>> GetItemsPage(
            int page, int pageSize,
            string search)
        {
            var productsPage = await productService.GetItemsPage(
                new PaginationArgs(page, pageSize),
                new ProductFilterArgs { Search = search }
            );
            var products = productsPage.ProductItems.Select(pv =>
                productMapper.MapToItemsResponse((pv, imageService.GetImageUri(pv.Id)))
            );
            var response = new ProductItemsPageResponse
            {
                FoundCount = productsPage.FoundCount,
                ProductItems = products
            };
            return Ok(response);
        }

        [HttpPost]
        [AuthorizeRole(AppUserRole.Employee)]
        public async Task<ActionResult<Guid>> Create(
            [FromForm] IFormFile image,
            [ModelBinder(typeof(JsonModelBinder))] CreateProductDto dto)
        {
            var productId = await productService.Create(dto);
            if (image is not null)
                await imageService.SaveImage(image, ImageType.Product, productId);
            return Ok(productId);
        }

        [HttpPut]
        [AuthorizeRole(AppUserRole.Employee)]
        public async Task<IActionResult> Update(
            [FromForm] IFormFile image,
            [ModelBinder(typeof(JsonModelBinder))] UpdateProductDto dto)
        {
            if (image is not null)
                await imageService.SaveImage(image, ImageType.Product, dto.Id);

            await productService.Update(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [AuthorizeRole(AppUserRole.Employee)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await productService.Delete(id);
            return NoContent();
        }
    }
}
