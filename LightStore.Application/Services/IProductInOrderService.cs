using LightStore.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LightStore.Application.Services
{
    public interface IProductInOrderService
    {
        Task<List<ProductInOrder>> LinkProductsInOrder(Guid orderId, IEnumerable<ProductInOrder> productsInOrder);
    }
}
