using MediatR;

namespace CleanArchitecture.Application.Features.AuthFeatures.Queries.Role.GetRoleById
{
    public sealed record GetRoleByIdQuery(string RoleId) : IRequest<GetRoleByIdQueryResponse>;
}