using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CleanArchitecture.Application.Abstracts;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Login;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Infrastructure.Authentication
{
    public sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtOptionsDto _jwtOptions;
        private readonly UserManager<AppUser> _userManager;

        public JwtProvider(IOptions<JwtOptionsDto> jwtOptions, UserManager<AppUser> userManager)
        {
            _jwtOptions = jwtOptions.Value;
            _userManager = userManager;
        }

        public async Task<LoginCommandResponse> CreateTokenAsync(AppUser appUser)
        {
            var claims = new Claim[]
            {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, appUser.Email ?? string.Empty),
                new Claim(ClaimTypes.Name, appUser.FullName ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, appUser.Id ?? string.Empty),
                new Claim(ClaimTypes.MobilePhone, appUser.PhoneNumber ?? string.Empty),
            };
            DateTime expires = DateTime.Now.AddHours(1);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: expires,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                    SecurityAlgorithms.HmacSha256));

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            appUser.RefreshToken = refreshToken;
            appUser.RefreshTokenExpires = expires.AddMinutes(15);
            await _userManager.UpdateAsync(appUser);

            LoginCommandResponse response = new(token, expires, refreshToken, appUser.RefreshTokenExpires);
            return response;
        }
    }
}