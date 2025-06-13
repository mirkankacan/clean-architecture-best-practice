using CleanArchitecture.Application.Features.AuthFeatures.Commands.Role.AssignRole;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Role.CreateRole;
using CleanArchitecture.Application.Features.AuthFeatures.Queries.Role.GetRoleById;
using CleanArchitecture.Application.Features.AuthFeatures.Queries.Role.GetRoles;
using CleanArchitecture.Infrastructure.Authorization;
using CleanArchitecture.WebApi.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers
{
    [RoleFilter("Admin")]
    public sealed class RolesController : ApiController
    {
        public RolesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateRoleCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Assign([FromBody] AssignRoleCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpGet("[action]/{roleId}")]
        public async Task<IActionResult> GetById(string roleId, CancellationToken cancellationToken)
        {
            GetRoleByIdQuery query = new(roleId);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRoles(CancellationToken cancellationToken)
        {
            GetRolesQuery query = new();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}