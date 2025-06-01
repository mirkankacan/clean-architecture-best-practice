using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Features.CarFeatures.Queries.GetCar
{
    public sealed record GetCarsQueryResponse(PaginatedResult<Car> PaginatedResult);
}