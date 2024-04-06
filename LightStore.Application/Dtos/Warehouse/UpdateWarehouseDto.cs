using System;
using System.ComponentModel.DataAnnotations;

namespace LightStore.Application.Dtos.Warehouse
{
    public class UpdateWarehouseDto
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
        [Required]
        [MaxLength(128)]
        public string Address { get; set; }
        public long PhoneNumber { get; set; }
    }
}
