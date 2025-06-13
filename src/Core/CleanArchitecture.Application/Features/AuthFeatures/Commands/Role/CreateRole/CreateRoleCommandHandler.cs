using CleanArchitecture.Application.Services;
using MediatR;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Role.CreateRole
{
    public sealed class CreateRoleCommandHandler(IRoleService roleService) : IRequestHandler<CreateRoleCommand, string>
    {
        public async Task<string> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
        {
            await roleService.CreateRoleAsync(command);
            return "Rol başarıyla oluşturuldu.";
        }
    }
}