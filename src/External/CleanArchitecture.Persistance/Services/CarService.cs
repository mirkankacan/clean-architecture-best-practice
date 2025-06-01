using AutoMapper;
using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetCar;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetCarById;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistance.Context;
using Microsoft.EntityFrameworkCore;

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

        public async Task<PaginatedResult<Car>> GetAsync(GetCarsQuery query, CancellationToken cancellationToken)
        {
            var queryable = context.Set<Car>().AsNoTracking();

            var count = await queryable.CountAsync(cancellationToken);

            var data = await queryable
                .Skip((query.PaginationRequest.PageIndex - 1) * query.PaginationRequest.PageSize)
                .Take(query.PaginationRequest.PageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedResult<Car>(query.PaginationRequest.PageIndex, query.PaginationRequest.PageSize, count, data);
        }

        public async Task<Car> GetByIdAsync(GetCarByIdQuery query, CancellationToken cancellationToken)
        {
            var car = await context.Set<Car>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);
            return car;
        }
    }
}