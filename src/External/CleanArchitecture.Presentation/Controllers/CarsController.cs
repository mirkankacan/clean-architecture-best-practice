using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Presentation.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers
{
    public sealed class CarsController : ApiController
    {
        public CarsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateCarCommand command, CancellationToken cancellationToken)
        {
            if (command == null)
            {
                return BadRequest("Gönderilen değer boş olamaz.");
            }
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}