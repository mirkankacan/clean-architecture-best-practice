﻿using FluentValidation;

namespace CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar
{
    public sealed class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
    {
        public CreateCarCommandValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Araç adı boş olamaz!");
            RuleFor(r => r.Model).MinimumLength(3).WithMessage("Araç adı en az 3 karakter olmalıdır!");

            RuleFor(r => r.Model).NotEmpty().WithMessage("Araç modeli boş olamaz!");
            RuleFor(r => r.Model).MinimumLength(3).WithMessage("Araç modeli en az 3 karakter olmalıdır!");

            RuleFor(r => r.EnginePower).NotEmpty().WithMessage("Araç motor gücü boş olamaz!");
            RuleFor(r => r.EnginePower).GreaterThan(0).WithMessage("Araç motor gücü 0'dan büyük olmalıdır!");
        }
    }
}