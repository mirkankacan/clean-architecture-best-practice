using CleanArchitecture.Application.Services;
using MediatR;

namespace CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar
{
    public sealed class CreateCarCommandHandler(ICarService carService) : IRequestHandler<CreateCarCommand, CreateCarCommandResponse>
    {
        public async Task<CreateCarCommandResponse> Handle(CreateCarCommand command, CancellationToken cancellationToken)
        {
            var createdId = await carService.CreateAsync(command, cancellationToken);
            return new CreateCarCommandResponse(createdId);
        }
    }
}