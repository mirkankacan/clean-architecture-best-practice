using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetCar;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetCarById;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.WebApi.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers
{
    public sealed class CarsController : ApiController
    {
        public CarsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateCarCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
        }

        [HttpGet("[action]/{pageIndex}/{pageSize}/{search}")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken, int pageIndex = 1, int pageSize = 12, string search = "")
        {
            var query = new GetCarsQuery(new PaginationRequest(pageIndex, pageSize, search));
            var response = await _mediator.Send(query, cancellationToken);
            return Ok(response);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            var query = new GetCarByIdQuery(id);
            var response = await _mediator.Send(query, cancellationToken);
            return Ok(response);
        }
    }
}