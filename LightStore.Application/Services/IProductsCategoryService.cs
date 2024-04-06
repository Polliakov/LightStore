using LightStore.Application.Dtos.ProductsCategory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LightStore.Application.Services
{
    public interface IProductsCategoryService
    {
        Task<ProductsCategoryVm> Get(Guid id);
        Task<List<ProductsCategoryVm>> GetAll();
        Task<Guid> Create(CreateProductsCategoryDto dto);
        Task Update(UpdateProductsCategoryDto dto);
        Task Delete(Guid id);
    }
}
