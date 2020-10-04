namespace Nudes.Paginator.Core
{
    public class PageRequest
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 30;

        public string Field { get; set; }

        public SortDirection? SortDirection { get; set; }

        public bool HasSortingData => Field != null && SortDirection.HasValue;
    }
}
