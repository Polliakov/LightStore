using LightStore.Persistence.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace LightStore.Application.Dtos.AppUser
{
    public class CreateAppUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(32)]
        public string Password { get; set; }
        public AppUserRole Role { get; set; }
    }
}
