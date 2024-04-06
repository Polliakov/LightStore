using LightStore.Application.Dtos.AppUser;
using LightStore.Persistence.Entities;
using Mapster;

namespace LightStore.Application.Mapping.Mappers
{
    [Mapper]
    public interface IAppUserMapper
    {
        AppUserVm MapToDto(AppUser appUser);
        AppUser MapFromDto(CreateAppUserDto appUser);
    }
}
