using LightStore.Application.Dtos.Warehouse;
using LightStore.Application.Services;
using LightStore.ImageService;
using LightStore.ImageService.Interfaces;
using LightStore.Persistence.Entities.Base;
using LightStore.WebApi.Attributes;
using LightStore.WebApi.Binding;
using LightStore.WebApi.Dtos.Warehouse;
using LightStore.WebApi.Mapping.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightStore.WebApi.Controllers
{
    public class WarehouseController : BaseController
    {
        public WarehouseController(IWarehouseService warehouseService,
                                   IWarehouseMapper warehouseMapper,
                                   IImageService imageService)
        {
            this.warehouseService = warehouseService;
            this.warehouseMapper = warehouseMapper;
            this.imageService = imageService;
        }

        private readonly IWarehouseService warehouseService;
        private readonly IWarehouseMapper warehouseMapper;
        private readonly IImageService imageService;

        [HttpGet("{id}")]
        public async Task<ActionResult<WarehouseResponse>> Get(Guid id)
        {
            var vm = await warehouseService.Get(id);
            var response = warehouseMapper.MapToResponse((vm, imageService.GetImageUri(vm.Id)));
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<WarehouseResponse>>> GetAll()
        {
            var vms = await warehouseService.GetAll();
            var response = vms.Select(vm =>
                warehouseMapper.MapToResponse((vm, imageService.GetImageUri(vm.Id)))
            );
            return Ok(response);
        }

        [HttpGet("Minified")]
        public async Task<ActionResult<List<WarehouseMinifiedVm>>> GetAllMinified()
        {
            return await warehouseService.GetAllMinified();
        }

        [HttpPost]
        [AuthorizeRole(AppUserRole.Admin)]
        public async Task<ActionResult<Guid>> Create(
            [FromForm] IFormFile image,
            [ModelBinder(typeof(JsonModelBinder))] CreateWarehouseDto dto)
        {
            var id = await warehouseService.Create(dto);
            if (image is not null)
                await imageService.SaveImage(image, ImageType.Warehouse, id);
            return Ok(id);
        }

        [HttpPut]
        [AuthorizeRole(AppUserRole.Admin)]
        public async Task<IActionResult> Update(
            [FromForm] IFormFile image,
            [ModelBinder(typeof(JsonModelBinder))] UpdateWarehouseDto dto)
        {
            await warehouseService.Update(dto);
            if (image is not null)
                await imageService.SaveImage(image, ImageType.Warehouse, dto.Id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [AuthorizeRole(AppUserRole.Admin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await warehouseService.Delete(id);
            return NoContent();
        }
    }
}
