using FluentValidation;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Login
{
    public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email alanı boş olamaz!")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz!");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre alanı boş olamaz!")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır!")
                .Matches("[A-Z]").WithMessage("Şifre en az 1 adet büyük karakter içermelidir!")
                .Matches("[a-z]").WithMessage("Şifre en az 1 adet küçük karakter içermelidir!")
                .Matches("[0-9]").WithMessage("Şifre en az 1 adet rakam içermelidir!")
                .Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az 1 adet özel karakter içermelidir!");
        }
    }
}