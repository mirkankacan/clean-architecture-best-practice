using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Role.AssignRole
{
    public sealed record AssignRoleCommand(string UserId, string RoleId) : IRequest<string>;
}