using FluentValidation;

namespace CleanArchitecture.Application.Features.CarFeatures.Queries.GetCarById
{
    public sealed class GetCarByIdQueryValidator : AbstractValidator<GetCarByIdQuery>
    {
        public GetCarByIdQueryValidator()
        {
            RuleFor(r => r.Id).NotEmpty().WithMessage("Id boş olamaz!");
        }
    }
}