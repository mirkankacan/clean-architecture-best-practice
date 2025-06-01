namespace CleanArchitecture.Domain.Dtos
{
    public sealed record PaginationRequest(int PageIndex = 0, int PageSize = 12);
}