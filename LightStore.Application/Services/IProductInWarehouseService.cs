using LightStore.Application.Dtos.ProductInWarehouse;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LightStore.Application.Services
{
    public interface IProductInWarehouseService
    {
        Task<List<ProductInWarehouseVm>> GetProductRemains(Guid productId);
        Task<List<ProductRemainsMinifiedVm>> GetProductsRemainsMinified(IEnumerable<Guid> productIds);
        Task AddCount(IEnumerable<IChangeProductCountDto> changeDtos, Guid emloyeeId);
        Task Writeoff(IEnumerable<IChangeProductCountDto> changeDtos, Guid warehouseId);
    }
}
