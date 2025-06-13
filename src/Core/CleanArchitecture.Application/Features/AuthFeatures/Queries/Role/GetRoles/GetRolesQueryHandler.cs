using CleanArchitecture.Application.Services;
using MediatR;

namespace CleanArchitecture.Application.Features.AuthFeatures.Queries.Role.GetRoles
{
    public sealed class GetRolesQueryHandler(IRoleService roleService) : IRequestHandler<GetRolesQuery, GetRolesQueryResponse>
    {
        public async Task<GetRolesQueryResponse> Handle(GetRolesQuery query, CancellationToken cancellationToken)
        {
            var roles = await roleService.GetRolesAsync();
            return roles;
        }
    }
}