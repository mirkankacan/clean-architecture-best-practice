using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;

namespace CleanArchitecture.Application.Services
{
    public interface ICarService
    {
        Task<string> CreateAsync(CreateCarCommand command, CancellationToken cancellationToken);
    }
}