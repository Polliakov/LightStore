using LightStore.Application.Dtos.ProductsCategory;
using LightStore.Application.Services;
using LightStore.Application.Utils;
using LightStore.ImageService;
using LightStore.ImageService.Interfaces;
using LightStore.Persistence.Entities.Base;
using LightStore.WebApi.Attributes;
using LightStore.WebApi.Binding;
using LightStore.WebApi.Dtos.ProductsCategory;
using LightStore.WebApi.Mapping.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LightStore.WebApi.Controllers
{
    public class ProductsCategoryController : BaseController
    {
        public ProductsCategoryController(IProductsCategoryService productsCategoryService,
                                          IImageService imageService,
                                          IProductsCategoryMapper productsCategoryMapper)
        {
            this.productsCategoryService = productsCategoryService;
            this.imageService = imageService;
            this.productsCategoryMapper = productsCategoryMapper;
        }

        private readonly IProductsCategoryService productsCategoryService;
        private readonly IImageService imageService;
        private readonly IProductsCategoryMapper productsCategoryMapper;

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductsCategoryResponse>> Get(Guid id)
        {
            var vm = await productsCategoryService.Get(id);
            var response = productsCategoryMapper.MapToResponse((vm, imageService.GetImageUri(vm.Id)));

            if (vm.ChildСategories is not null)
            {
                var childrenResponse = new List<ProductsCategoryResponse>(vm.ChildСategories.Count);
                foreach (var childVm in vm.ChildСategories)
                    childrenResponse.Add(productsCategoryMapper.MapToResponse((
                        childVm, imageService.GetImageUri(childVm.Id)
                    )));
                response.ChildСategories = childrenResponse;
            }

            return response;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductsCategoryResponse>>> GetAll()
        {
            var vms = await productsCategoryService.GetAll();
            var response = new List<ProductsCategoryResponse>(vms.Count);
            foreach (var vm in vms)
            {
                response.Add(
                    TreeUtils.Transform(vm, src => src.ChildСategories,
                        src => productsCategoryMapper.MapToResponse((
                            src,
                            imageService.GetImageUri(src.Id)
                        )),
                        (dest, children) => dest.ChildСategories = children
                    )
                );
            }
            return Ok(response);
        }

        [HttpPost]
        [AuthorizeRole(AppUserRole.Employee)]
        public async Task<ActionResult<Guid>> Create(
            [FromForm] IFormFile image,
            [ModelBinder(typeof(JsonModelBinder))] CreateProductsCategoryDto dto)
        {
            var categoryId = await productsCategoryService.Create(dto);
            if (image is not null)
                await imageService.SaveImage(image, ImageType.Category, categoryId);
            return Ok(categoryId);
        }

        [HttpPut]
        [AuthorizeRole(AppUserRole.Employee)]
        public async Task<IActionResult> Update(
            [FromForm] IFormFile image,
            [ModelBinder(typeof(JsonModelBinder))] UpdateProductsCategoryDto dto)
        {
            await productsCategoryService.Update(dto);
            if (image is not null)
                await imageService.SaveImage(image, ImageType.Category, dto.Id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [AuthorizeRole(AppUserRole.Employee)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await productsCategoryService.Delete(id);
            return NoContent();
        }
    }
}
