using LightStore.Application.Dtos.ProductInAdding;
using LightStore.Application.Dtos.ProductsAdding;
using LightStore.Application.Services;
using LightStore.ImageService.Interfaces;
using LightStore.Persistence.Entities.Base;
using LightStore.WebApi.Attributes;
using LightStore.WebApi.Dtos.ProductInAdding;
using LightStore.WebApi.Mapping.Mappers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightStore.WebApi.Controllers
{
    public class ProductsAddingController : BaseController
    {
        public ProductsAddingController(IProductsAddingService productsAddingService,
                                        IProductsAddingMapper productsAddingMapper,
                                        IProductInAddingMapper productInAddingMapper,
                                        IImageService imageService)
        {
            this.productsAddingService = productsAddingService;
            this.productsAddingMapper = productsAddingMapper;
            this.productInAddingMapper = productInAddingMapper;
            this.imageService = imageService;
        }

        private readonly IProductsAddingService productsAddingService;
        private readonly IProductsAddingMapper productsAddingMapper;
        private readonly IProductInAddingMapper productInAddingMapper;
        private readonly IImageService imageService;

        [HttpGet("{id}")]
        [AuthorizeRole(AppUserRole.Admin, AppUserRole.Employee)]
        public async Task<ActionResult<List<ProductsAddingDetailsVm>>> GetDetails(Guid id)
        {
            var adding = await productsAddingService.GetDetails(id);
            var response = productsAddingMapper.MapToDetailsResponse(adding);

            response.ProductsInAdding = ProductsVmToResponse(adding.ProductsInAdding);
            
            return Ok(response);
        }

        [HttpGet]
        [AuthorizeRole(AppUserRole.Admin, AppUserRole.Employee)]
        public async Task<ActionResult<List<ProductsAddingVm>>> GetAll()
        {
            var vms = await productsAddingService.GetAll();
            return Ok(vms);
        }

        [HttpPost]
        [AuthorizeRole(AppUserRole.Employee)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateProductsAddingDto dto)
        {
            var id = await productsAddingService.Create(dto, UserId);
            return Ok(id);
        }

        private IEnumerable<ProductInAddingResponse> ProductsVmToResponse(List<ProductInAddingVm> vms)
        {
            if (vms is null) return null;

            return vms.Select(vm => productInAddingMapper.MapToResponse((
                vm,
                imageService.GetImageUri(vm.ProductId)
            )));
        }
    }
}
