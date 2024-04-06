using System.ComponentModel.DataAnnotations;

namespace LightStore.Application.Dtos.ProductInOrder
{
    public class CreateDeliveryInformationDto
    {
        [Required]
        [MaxLength(128)]
        public string Address { get; set; }
        public long PhoneNumber { get; set; }
    }
}
