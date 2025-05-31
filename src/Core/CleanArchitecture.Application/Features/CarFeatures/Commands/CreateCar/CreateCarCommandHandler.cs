using CleanArchitecture.Application.Services;
using MediatR;

namespace CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar
{
    public sealed class CreateCarCommandHandler(ICarService carService) : IRequestHandler<CreateCarCommand, CreateCarResponse>
    {
        public async Task<CreateCarResponse> Handle(CreateCarCommand command, CancellationToken cancellationToken)
        {
            var createdId = await carService.CreateAsync(command, cancellationToken);
            return new CreateCarResponse(createdId);
        }
    }
}