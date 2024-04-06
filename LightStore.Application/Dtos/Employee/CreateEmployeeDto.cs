using LightStore.Persistence.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace LightStore.Application.Dtos.Employee
{
    public class CreateEmployeeDto
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
        public AppUserRole Role { get; set; }
    }
}
