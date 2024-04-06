using System;
using System.Threading.Tasks;

namespace LightStore.Application.Services
{
    public interface IProductPriceService
    {
        Task<Guid> Create(Guid productId, decimal price);
    }
}
