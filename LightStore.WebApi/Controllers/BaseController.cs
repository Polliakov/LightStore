using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace LightStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected Guid UserId => User.Identity.IsAuthenticated ?
            Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) :
            Guid.Empty;
    }
}
