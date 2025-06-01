using CleanArchitecture.Domain.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Features.CarFeatures.Queries.GetCar
{
    public sealed record GetCarsQuery(PaginationRequest PaginationRequest) : IRequest<GetCarsQueryResponse>;
}