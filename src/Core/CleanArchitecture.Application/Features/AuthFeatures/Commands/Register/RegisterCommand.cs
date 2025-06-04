using MediatR;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Register
{
    public sealed record RegisterCommand(string Email, string Password, string Fullname) : IRequest<string>;
}