using LightStore.Application.Dtos.AppUser;

namespace LightStore.WebApi.Services
{
    public interface ITokenService
    {
        string GenerateToken(AppUserVm appUser);
    }
}
