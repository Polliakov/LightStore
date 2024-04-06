using LightStore.Persistence.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace LightStore.Application.Dtos.AppUser
{
    public class UpdateAppUserDto
    {
        public Guid AppUserId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(32)]
        public string Password { get; set; }
        public AppUserRole? Role { get; set; }
    }
}
