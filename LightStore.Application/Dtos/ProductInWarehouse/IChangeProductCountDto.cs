using System;

namespace LightStore.Application.Dtos.ProductInWarehouse
{
    public interface IChangeProductCountDto
    {
        public Guid ProductId { get; }
        public int Count { get; }
    }
}
