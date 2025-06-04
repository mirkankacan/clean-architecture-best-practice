using CleanArchitecture.Application.Services;
using MediatR;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Register
{
    public sealed class RegisterCommandHandler(IAuthService authService) : IRequestHandler<RegisterCommand, string>
    {
        public async Task<string> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            await authService.RegisterAsync(command);
            return "Kullanıcı başarıyla kayıt oldu.";
        }
    }
}