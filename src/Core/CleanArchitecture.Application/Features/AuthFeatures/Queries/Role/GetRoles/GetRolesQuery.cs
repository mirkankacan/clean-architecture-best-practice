using MediatR;

namespace CleanArchitecture.Application.Features.AuthFeatures.Queries.Role.GetRoles
{
    public sealed record GetRolesQuery() : IRequest<GetRolesQueryResponse>;
}