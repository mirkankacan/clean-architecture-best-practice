using MediatR;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Role.CreateRole
{
    public sealed record CreateRoleCommand(string RoleName) : IRequest<string>;
}