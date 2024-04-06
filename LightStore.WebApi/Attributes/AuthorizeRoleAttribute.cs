using LightStore.Persistence.Entities.Base;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace LightStore.WebApi.Attributes
{
    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        public AuthorizeRoleAttribute(params AppUserRole[] roles) : base()
        {
            if (roles.Length > 0)
                Roles = string.Join(", ", roles.Select(r => r.ToString()));
        }
    }
}
