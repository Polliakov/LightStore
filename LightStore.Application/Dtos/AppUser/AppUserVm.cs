using LightStore.Persistence.Entities.Base;
using System;

namespace LightStore.Application.Dtos.AppUser
{
    public class AppUserVm
    {
        public Guid AppUserId { get; set; }
        public string Email { get; set; }
        public AppUserRole Role { get; set; }
    }
}
