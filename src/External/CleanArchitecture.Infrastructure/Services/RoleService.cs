using CleanArchitecture.Application.Features.AuthFeatures.Commands.Role.AssignRole;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Role.CreateRole;
using CleanArchitecture.Application.Features.AuthFeatures.Queries.Role.GetRoleById;
using CleanArchitecture.Application.Features.AuthFeatures.Queries.Role.GetRoles;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Services
{
    public sealed class RoleService(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager) : IRoleService
    {
        public async Task AssignRoleAsync(AssignRoleCommand command)
        {
            var user = await userManager.FindByIdAsync(command.UserId);
            if (user == null)
            {
                throw new UserNotFoundException($"ID'si {command.UserId} olan kullanıcı bulunamadı.");
            }

            var role = await roleManager.FindByIdAsync(command.RoleId);
            if (role == null)
            {
                throw new ArgumentException($"ID'si {command.RoleId} olan rol bulunamadı.");
            }

            var currentRoles = await userManager.GetRolesAsync(user);

            if (currentRoles.Any())
            {
                var removeResult = await userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded)
                {
                    throw new InvalidOperationException($"Mevcut rolleri silme işlemi başarısız: {string.Join(", ", removeResult.Errors.Select(e => e.Description))}");
                }
            }

            var result = await userManager.AddToRoleAsync(user, role.Name);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Rol atama işlemi başarısız: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }

        public async Task CreateRoleAsync(CreateRoleCommand command)
        {
            var existingRole = await roleManager.FindByNameAsync(command.RoleName);
            if (existingRole != null)
            {
                throw new InvalidOperationException($"'{command.RoleName}' adlı rol zaten mevcut.");
            }

            AppRole role = new AppRole
            {
                Name = command.RoleName,
            };

            var result = await roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Rol oluşturma işlemi başarısız: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }

        public async Task<GetRoleByIdQueryResponse> GetRoleByIdAsync(GetRoleByIdQuery query)
        {
            var role = await roleManager.FindByIdAsync(query.RoleId);
            if (role == null)
            {
                throw new ArgumentException($"ID'si {query.RoleId} olan rol bulunamadı.");
            }
            GetRoleDto getRoleDto = new GetRoleDto(role.Id, role.Name);
            return new GetRoleByIdQueryResponse(getRoleDto);
        }

        public async Task<GetRolesQueryResponse> GetRolesAsync()
        {
            var roles = await roleManager.Roles.ToListAsync();

            var roleList = roles.Select(role => new GetRoleDto(role.Id, role.Name)).ToList();

            return new GetRolesQueryResponse(roleList);
        }

        public async Task<bool> UserIsInRole(string userId, string roleName)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            var userInRole = await userManager.GetUsersInRoleAsync(roleName);
            if (userInRole == null || !userInRole.Any())
            {
                return false;
            }
            return true;
        }
    }
}