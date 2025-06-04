using AutoMapper;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Register;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Persistance.Services
{
    public sealed class AuthService(UserManager<AppUser> userManager, IMapper mapper) : IAuthService
    {
        public Task<bool> LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
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
        }
    }
}