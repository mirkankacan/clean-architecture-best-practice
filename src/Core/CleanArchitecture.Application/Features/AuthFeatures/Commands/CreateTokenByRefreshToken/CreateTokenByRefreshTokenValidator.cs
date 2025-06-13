using FluentValidation;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.CreateTokenByRefreshToken
{
    public sealed class CreateTokenByRefreshTokenValidator : AbstractValidator<CreateTokenByRefreshTokenCommand>
    {
        public CreateTokenByRefreshTokenValidator()
        {
            RuleFor(r => r.UserId).NotEmpty().WithMessage("User ID boş olamaz!");
            RuleFor(r => r.RefreshToken)
                .NotEmpty().WithMessage("Refresh Token boş olamaz!")
                    .Length(44).WithMessage("Refresh Token 44 karakter olmalıdır!");
        }
    }
}