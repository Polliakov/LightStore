using System;
using System.ComponentModel.DataAnnotations;

namespace LightStore.Application.Dtos.ProductsCategory
{
    public class CreateProductsCategoryDto
    {
        public Guid? ParentCategoryId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
