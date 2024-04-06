using Microsoft.IdentityModel.Tokens;
using System;

namespace LightStore.WebApi.Options
{
    public class AuthOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
        public int TokenLifetime { get; set; }
        public SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new(Convert.FromBase64String(Secret));
    }
}
