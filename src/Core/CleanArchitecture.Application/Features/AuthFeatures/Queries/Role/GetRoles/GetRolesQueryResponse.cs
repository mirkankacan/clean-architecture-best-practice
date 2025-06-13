using CleanArchitecture.Domain.Dtos;

namespace CleanArchitecture.Application.Features.AuthFeatures.Queries.Role.GetRoles
{
    public sealed record GetRolesQueryResponse(IEnumerable<GetRoleDto> Roles);
}