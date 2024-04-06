using LightStore.Application.Dtos.ProductInWarehouse;
using LightStore.Application.Validation.Attributes;
using System;

namespace LightStore.Application.Dtos.ProductInAdding
{
    public class ProductInAddingDto : IChangeProductCountDto
    {
        public Guid ProductId { get; set; }
        [PositiveInteger]
        public int Count { get; set; }
    }
}
