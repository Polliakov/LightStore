using LightStore.Persistence.Entities.Base;
using System;

namespace LightStore.Application.Dtos.Product
{
    public class ProductDetailsVm
    {
        public Guid? CategoryId { get; set; }
        public string Title { get; set; }
        public int AvailabilityCount { get; set; }
        public decimal Price { get; set; }
        public double? Weight { get; set; }
        public string Size { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public string Description { get; set; }
    }
}
