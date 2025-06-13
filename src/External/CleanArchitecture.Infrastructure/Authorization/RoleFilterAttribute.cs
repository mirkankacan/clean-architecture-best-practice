using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Infrastructure.Authorization
{
    public sealed class RoleFilterAttribute : TypeFilterAttribute
    {
        public RoleFilterAttribute(params string[] roleNames) : base(typeof(RoleAttribute))
        {
            Arguments = new object[] { roleNames };
        }
    }
}