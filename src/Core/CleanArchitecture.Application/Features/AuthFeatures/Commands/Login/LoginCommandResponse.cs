namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Login
{
    public sealed record LoginCommandResponse(string Token, DateTime TokenExpires, string RefreshToken, DateTime? RefreshTokenExpires);
}