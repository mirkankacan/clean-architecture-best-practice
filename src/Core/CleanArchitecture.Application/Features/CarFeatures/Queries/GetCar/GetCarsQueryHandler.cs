using CleanArchitecture.Application.Services;
using MediatR;

namespace CleanArchitecture.Application.Features.CarFeatures.Queries.GetCar
{
    public sealed class GetCarsQueryHandler(ICarService carService) : IRequestHandler<GetCarsQuery, GetCarsQueryResponse>
    {
        public async Task<GetCarsQueryResponse> Handle(GetCarsQuery query, CancellationToken cancellationToken)
        {
            var cars = await carService.GetAsync(query, cancellationToken);
            return new GetCarsQueryResponse(cars);
        }
    }
}