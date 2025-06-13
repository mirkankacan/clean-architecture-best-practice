using System.Security.Claims;
using CleanArchitecture.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanArchitecture.Infrastructure.Authorization
{
    public sealed class RoleAttribute(string[] roles, IRoleService roleService) : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var isInRole = roles.Any(role => roleService.UserIsInRole(userId, role).GetAwaiter().GetResult());
            if (!isInRole)
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}