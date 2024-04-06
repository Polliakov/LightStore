using System;
using System.ComponentModel.DataAnnotations;

namespace LightStore.Application.Dtos.Customer
{
    public class CreateCustomerDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(32)]
        public string Password { get; set; }
        [Required]
        [MaxLength(128)]
        public string Surname { get; set; }
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        [MaxLength(128)]
        public string Patronymic { get; set; }
        public Guid? CartId { get; set; }
    }
}
