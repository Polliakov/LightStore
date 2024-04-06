using LightStore.Application.Dtos.AppUser;

namespace LightStore.WebApi.Dtos.AppUser
{
    public class AuthenticationResponse
    {
        public AppUserVm AppUser { get; set; }
        public string Token { get; set; }
    }
}
