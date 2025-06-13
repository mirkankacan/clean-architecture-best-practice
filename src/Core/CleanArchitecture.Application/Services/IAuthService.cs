using CleanArchitecture.Application.Features.AuthFeatures.Commands.CreateTokenByRefreshToken;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Login;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Register;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Role.AssignRole;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Role.CreateRole;

namespace CleanArchitecture.Application.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterCommand command);

        Task<LoginCommandResponse> LoginAsync(LoginCommand command);

        Task<LoginCommandResponse> CreateTokenByRefreshTokenAsync(CreateTokenByRefreshTokenCommand command);
    }
}