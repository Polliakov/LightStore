using LightStore.Application.Dtos.Pagination;
using LightStore.Application.Dtos.Product;
using LightStore.Application.Filtering.Product;
using System;
using System.Threading.Tasks;

namespace LightStore.Application.Services
{
    public interface IProductService
    {
        Task<ProductItemsPageVm> GetItemsPage(PaginationArgs pagination, ProductFilterArgs filtering);
        Task<ProductsPageVm> GetPage(PaginationArgs pagination, ProductFilterArgs filtering);
        Task<ProductDetailsVm> GetDetails(Guid id);
        Task<Guid> Create(CreateProductDto productDto);
        Task Update(UpdateProductDto productDto);
        Task Delete(Guid id);
    }
}
