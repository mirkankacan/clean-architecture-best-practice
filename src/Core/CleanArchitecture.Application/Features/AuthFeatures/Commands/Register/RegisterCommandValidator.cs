using FluentValidation;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Register
{
    public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
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

            RuleFor(x => x.Fullname)
                .NotEmpty().WithMessage("Ad ve soyad alanı boş olamaz!")
                .MinimumLength(3).WithMessage("Ad ve soyad en az 3 karakter olmalıdır!");
        }
    }
}