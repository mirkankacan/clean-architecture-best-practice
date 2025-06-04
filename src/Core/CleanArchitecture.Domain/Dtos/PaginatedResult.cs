namespace CleanArchitecture.Domain.Dtos
{
    public sealed class PaginatedResult<TEntity>(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data) where TEntity : class
    {
        public int PageIndex { get; } = pageIndex;
        public int PageSize { get; } = pageSize;
        public long Count { get; } = count;
        public IEnumerable<TEntity> Data { get; } = data;
        public int TotalPages => (int)Math.Ceiling((double)Count / PageSize);

        public bool IsFirstPage => PageIndex == 1;
        public bool IsLastPage => PageIndex >= Math.Ceiling((double)Count / PageSize);
    }
}