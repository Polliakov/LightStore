using LightStore.Application.Dtos.ProductsAdding;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LightStore.Application.Services
{
    public interface IProductsAddingService
    {
        Task<ProductsAddingDetailsVm> GetDetails(Guid id);
        Task<List<ProductsAddingVm>> GetAll();
        Task<Guid> Create(CreateProductsAddingDto dto, Guid EmployeeId);
    }
}
