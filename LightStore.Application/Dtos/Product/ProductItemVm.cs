using LightStore.Persistence.Entities.Base;
using System;

namespace LightStore.Application.Dtos.Product
{
    public class ProductItemVm
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
    }
}
