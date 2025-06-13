using CleanArchitecture.Application.Services;
using MediatR;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Role.AssignRole
{
    public sealed class AssignRoleCommandHandler(IRoleService roleService) : IRequestHandler<AssignRoleCommand, string>
    {
        public async Task<string> Handle(AssignRoleCommand command, CancellationToken cancellationToken)
        {
            await roleService.AssignRoleAsync(command);
            return "Rol başarıyla atandı.";
        }
    }
}