using MediatR;

namespace CleanArchitecture.Application.Features.CarFeatures.Queries.GetCarById
{
    public sealed record GetCarByIdQuery(string Id) : IRequest<GetCarByIdQueryResponse>;
}