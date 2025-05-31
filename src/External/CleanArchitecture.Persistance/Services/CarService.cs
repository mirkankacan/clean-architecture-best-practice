using AutoMapper;
using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistance.Context;

namespace CleanArchitecture.Persistance.Services
{
    public sealed class CarService(AppDbContext context, IMapper mapper) : ICarService
    {
        public async Task<string> CreateAsync(CreateCarCommand command, CancellationToken cancellationToken)
        {
            var car = mapper.Map<Car>(command);
            await context.Set<Car>().AddAsync(car, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return car.Id.ToString();
        }
    }
}