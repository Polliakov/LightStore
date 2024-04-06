using LightStore.Application.Dtos.AppUser;
using LightStore.Application.Services;
using LightStore.WebApi.Dtos.AppUser;
using LightStore.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Threading.Tasks;

namespace LightStore.WebApi.Controllers
{
    public class AppUserController : BaseController
    {
        public AppUserController(IAppUserService appUserService,
                                 ITokenService tokenService)
        {
            this.appUserService = appUserService;
            this.tokenService = tokenService;
        }

        private readonly IAppUserService appUserService;
        private readonly ITokenService tokenService;

        [HttpGet("Authed")]
        [Authorize]
        public async Task<ActionResult<AppUserVm>> GetAuthorized()
        {
            var vm = await appUserService.Get(UserId);
            return Ok(vm);
        }

        [HttpPost("SignIn")]
        public async Task<ActionResult<AuthenticationResponse>> SignIn([FromBody] AppUserAuthDto authDto)
        {
            var vm = await appUserService.Authenticate(authDto);
            var token = tokenService.GenerateToken(vm);
            var response = new AuthenticationResponse { AppUser = vm, Token = token };
            return Ok(response);
        }

        [HttpPost("PasswordChange")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            await appUserService.ChangePassword(UserId, dto);
            return Ok();
        }
    }
}
