using CleanArchitecture.Application.Features.AuthFeatures.Commands.Login;
using CleanArchitecture.Application.Services;
using MediatR;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.CreateTokenByRefreshToken
{
    internal class CreateTokenByRefreshTokenHandler(IAuthService authService) : IRequestHandler<CreateTokenByRefreshTokenCommand, LoginCommandResponse>
    {
        public async Task<LoginCommandResponse> Handle(CreateTokenByRefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var response = await authService.CreateTokenByRefreshTokenAsync(command);
            return response;
        }
    }
}