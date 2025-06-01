using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetCar;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetCarById;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Services
{
    public interface ICarService
    {
        Task<string> CreateAsync(CreateCarCommand command, CancellationToken cancellationToken);

        Task<PaginatedResult<Car>> GetAsync(GetCarsQuery query, CancellationToken cancellationToken);

        Task<Car> GetByIdAsync(GetCarByIdQuery query, CancellationToken cancellationToken);
    }
}