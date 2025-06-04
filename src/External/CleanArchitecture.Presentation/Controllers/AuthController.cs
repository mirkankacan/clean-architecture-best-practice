using CleanArchitecture.Application.Features.AuthFeatures.Commands.Register;
using CleanArchitecture.Presentation.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers
{
    public sealed class AuthController : ApiController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
        {
            string result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}