using System.ComponentModel.DataAnnotations;

namespace LightStore.Application.Dtos.AppUser
{
    public class ChangePasswordDto
    {
        [Required]
        [MaxLength(32)]
        public string OldPassword { get; set; }
        [Required]
        [MaxLength(32)]
        public string NewPassword { get; set; }
    }
}
