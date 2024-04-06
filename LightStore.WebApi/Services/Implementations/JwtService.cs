using LightStore.Application.Dtos.AppUser;
using LightStore.WebApi.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LightStore.WebApi.Services.Implementations
{
    public class JwtService : ITokenService
    {
        public JwtService(IOptionsSnapshot<AuthOptions> authOptions)
        {
            this.authOptions = authOptions.Value;
        }

        private readonly AuthOptions authOptions;
        private readonly JwtSecurityTokenHandler tokenHandler = new();

        public string GenerateToken(AppUserVm appUser)
        {
            var securityKey = authOptions.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
                new Claim(JwtRegisteredClaimNames.Sub, appUser.AppUserId.ToString()),
                new Claim("role", appUser.Role.ToString())
            };

            var token = new JwtSecurityToken(
                authOptions.Issuer,
                authOptions.Audience,
                claims,
                expires: DateTime.UtcNow.AddSeconds(authOptions.TokenLifetime),
                signingCredentials: credentials);

            return tokenHandler.WriteToken(token);
        }
    }
}
