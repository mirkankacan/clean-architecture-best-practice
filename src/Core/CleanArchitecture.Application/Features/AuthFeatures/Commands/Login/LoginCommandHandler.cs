using CleanArchitecture.Application.Services;
using MediatR;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Login
{
    public sealed class LoginCommandHandler(IAuthService authService) : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        public async Task<LoginCommandResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var response = await authService.LoginAsync(command);
            return response;
        }
    }
}