using System;

namespace Nudes.Paginator.Core
{
    /// <summary>
    /// Contains all pagination data like page number and size
    /// </summary>
    public class PaginationData
    {
        /// <summary>
        /// Current Page number, page starts at 1
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Number of items per page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// The Sorting field
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// The Sorting direction
        /// </summary>
        public SortDirection? SortDirection { get; set; }

        /// <summary>
        /// Total number of items
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        /// Total number of pages
        /// </summary>
        public int PageCount => PageSize == 0 ? 0 : (int)Math.Ceiling(Total / (double)PageSize);

        /// <summary>
        /// If it is the first page
        /// </summary>
        public bool IsFirstPage => Page <= 1;

        /// <summary>
        /// If it is the last page
        /// </summary>
        public bool IsLastPage => PageCount <= Page;


        public PaginationData()
        { }


        public PaginationData(PageRequest request, long total)
        {
            Page = request.Page;
            PageSize = request.PageSize;
            Field = request.Field;
            SortDirection = request.SortDirection;
            Total = total;
        }

        public PaginationData(PageRequest request, long total, string field, SortDirection direction) : this(request, total)
        {
            Field = field;
            SortDirection = direction;
        }
    }
}
