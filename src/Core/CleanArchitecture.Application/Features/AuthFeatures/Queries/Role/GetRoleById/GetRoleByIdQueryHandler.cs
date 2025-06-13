using CleanArchitecture.Application.Services;
using MediatR;

namespace CleanArchitecture.Application.Features.AuthFeatures.Queries.Role.GetRoleById
{
    public sealed class GetRoleByIdQueryHandler(IRoleService roleService) : IRequestHandler<GetRoleByIdQuery, GetRoleByIdQueryResponse>
    {
        public async Task<GetRoleByIdQueryResponse> Handle(GetRoleByIdQuery query, CancellationToken cancellationToken)
        {
            var role = await roleService.GetRoleByIdAsync(query);
            return role;
        }
    }
}