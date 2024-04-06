namespace LightStore.Application.Dtos.Pagination
{
    public class PaginationArgs
    {
        public PaginationArgs() { }

        public PaginationArgs(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }

        public int Page { get; init; }
        public int PageSize { get; init; }
    }
}
