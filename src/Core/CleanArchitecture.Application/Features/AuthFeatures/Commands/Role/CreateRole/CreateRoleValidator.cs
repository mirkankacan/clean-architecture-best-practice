using FluentValidation;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Role.CreateRole
{
    public sealed class CreateRoleValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleValidator()
        {
            RuleFor(r => r.RoleName).NotEmpty().WithMessage("Rol adı boş olamaz!");
        }
    }
}