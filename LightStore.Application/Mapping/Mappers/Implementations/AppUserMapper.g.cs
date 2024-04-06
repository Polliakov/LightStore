using LightStore.Application.Dtos.AppUser;
using LightStore.Application.Mapping.Mappers;
using LightStore.Persistence.Entities;

namespace LightStore.Application.Mapping.Mappers.Implementations
{
    public partial class AppUserMapper : IAppUserMapper
    {
        public AppUserVm MapToDto(AppUser p1)
        {
            return p1 == null ? null : new AppUserVm()
            {
                AppUserId = p1.AppUserId,
                Email = p1.Email,
                Role = p1.Role
            };
        }
        public AppUser MapFromDto(CreateAppUserDto p2)
        {
            return p2 == null ? null : new AppUser()
            {
                Email = p2.Email,
                Role = p2.Role
            };
        }
    }
}