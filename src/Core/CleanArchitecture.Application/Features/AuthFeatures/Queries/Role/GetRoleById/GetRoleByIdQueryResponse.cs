using CleanArchitecture.Domain.Dtos;

namespace CleanArchitecture.Application.Features.AuthFeatures.Queries.Role.GetRoleById
{
    public sealed record GetRoleByIdQueryResponse(GetRoleDto Role);
}