using CleanArchitecture.Application.Services;
using MediatR;

namespace CleanArchitecture.Application.Features.CarFeatures.Queries.GetCarById
{
    public sealed class GetCarByIdQueryHandler(ICarService carService) : IRequestHandler<GetCarByIdQuery, GetCarByIdQueryResponse>
    {
        public async Task<GetCarByIdQueryResponse> Handle(GetCarByIdQuery query, CancellationToken cancellationToken)
        {
            var car = await carService.GetByIdAsync(query, cancellationToken);
            return new GetCarByIdQueryResponse(car);
        }
    }
}