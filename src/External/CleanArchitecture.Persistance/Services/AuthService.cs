using AutoMapper;
using CleanArchitecture.Application.Abstracts;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.CreateTokenByRefreshToken;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Login;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Register;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Persistance.Services
{
    public sealed class AuthService(UserManager<AppUser> userManager, IMapper mapper, IEmailService emailService, IJwtProvider jwtProvider) : IAuthService
    {
        public async Task<LoginCommandResponse> CreateTokenByRefreshTokenAsync(CreateTokenByRefreshTokenCommand command)
        {
            AppUser appUser = await userManager.FindByIdAsync(command.UserId);
            if (appUser == null)
                throw new UserNotFoundException(command.UserId);

            if (appUser.RefreshToken != command.RefreshToken || appUser.RefreshTokenExpires < DateTime.Now)
                throw new Exception("Geçersiz veya süresi dolmuş refresh token.");

            LoginCommandResponse response = await jwtProvider.CreateTokenAsync(appUser);
            return response;
        }

        public async Task<LoginCommandResponse> LoginAsync(LoginCommand command)
        {
            AppUser appUser = await userManager.FindByEmailAsync(command.Email);
            if (appUser == null)
                throw new UserNotFoundException(command.Email);

            var passResult = await userManager.CheckPasswordAsync(appUser, command.Password);

            if (!passResult)
                throw new InvalidCredentialsException();

            LoginCommandResponse response = await jwtProvider.CreateTokenAsync(appUser);
            return response;
        }

        public async Task RegisterAsync(RegisterCommand command)
        {
            AppUser user = mapper.Map<AppUser>(command);
            user.UserName = command.Email;
            IdentityResult result = await userManager.CreateAsync(user, command.Password);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            var emailResult = await emailService.SendEmailAsync(
                to: user.Email,
                subject: "Hesap Oluşturuldu ✅",
                body: $"Merhaba {user.FullName}, hesabınız başarılı bir şekilde oluşturuldu.",
                isHtml: true
            );
        }
    }
}