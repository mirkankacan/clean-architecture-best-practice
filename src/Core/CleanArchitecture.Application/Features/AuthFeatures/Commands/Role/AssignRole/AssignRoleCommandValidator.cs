using FluentValidation;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Role.AssignRole
{
    public sealed class AssignRoleCommandValidator : AbstractValidator<AssignRoleCommand>
    {
        public AssignRoleCommandValidator()
        {
            RuleFor(r => r.RoleId).NotEmpty().WithMessage("Rol ID boş olamaz!");
            RuleFor(r => r.UserId).NotEmpty().WithMessage("Kullanıcı ID boş olamaz!");
        }
    }
}