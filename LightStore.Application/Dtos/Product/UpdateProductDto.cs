using LightStore.Application.Validation.Attributes;
using LightStore.Persistence.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace LightStore.Application.Dtos.Product
{
    public class UpdateProductDto
    {
        public Guid Id { get; set; }
        public Guid? CategoryId { get; set; }
        [Required]
        [MaxLength(128)]
        public string Title { get; set; }
        [Precision(12, 2)]
        public decimal Price { get; set; }
        public double? Weight { get; set; }
        [MaxLength(64)]
        public string Size { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public string Description { get; set; }
    }
}
