using System.ComponentModel.DataAnnotations;

namespace LightStore.Application.Dtos.Customer
{
    public class UpdateCustomerDto
    {
        [Required]
        [MaxLength(128)]
        public string Surname { get; set; }
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        [MaxLength(128)]
        public string Patronymic { get; set; }
    }
}
