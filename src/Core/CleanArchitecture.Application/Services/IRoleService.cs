using CleanArchitecture.Application.Features.AuthFeatures.Commands.Role.AssignRole;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Role.CreateRole;
using CleanArchitecture.Application.Features.AuthFeatures.Queries.Role.GetRoleById;
using CleanArchitecture.Application.Features.AuthFeatures.Queries.Role.GetRoles;

namespace CleanArchitecture.Application.Services
{
    public interface IRoleService
    {
        Task CreateRoleAsync(CreateRoleCommand command);

        Task AssignRoleAsync(AssignRoleCommand command);

        Task<GetRolesQueryResponse> GetRolesAsync();

        Task<GetRoleByIdQueryResponse> GetRoleByIdAsync(GetRoleByIdQuery query);

        Task<bool> UserIsInRole(string userId, string roleName);
    }
}