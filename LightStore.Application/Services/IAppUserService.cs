using LightStore.Application.Dtos.AppUser;
using System;
using System.Threading.Tasks;

namespace LightStore.Application.Services
{
    public interface IAppUserService
    {
        Task<AppUserVm> Get(Guid id);
        Task<AppUserVm> Authenticate(AppUserAuthDto authDto);
        Task ChangePassword(Guid AppUserId, ChangePasswordDto changePasswordDto);
        Task<AppUserVm> Create(CreateAppUserDto appUserDto);
        Task Update(UpdateAppUserDto appUserDto);
        Task Delete(Guid Id);
    }
}
