using System.ComponentModel.DataAnnotations;

namespace LightStore.Application.Dtos.AppUser
{
    public class AppUserAuthDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
