using System.Collections.Generic;

namespace Nudes.Paginator.Core
{
    /// <summary>
    /// Base class to all paginated result
    /// </summary>
    /// <typeparam name="T">Type of paginated items</typeparam>
    public class PageResult<T> where T : class
    {
        /// <summary>
        /// Pagination information
        /// </summary>
        public virtual PaginationData Pagination { get; set; }

        /// <summary>
        /// Items that have been paginated
        /// </summary>
        public virtual ICollection<T> Items { get; set; }

        public PageResult()
        {
            Pagination = new PaginationData();
            Items = new List<T>();
        }

        public PageResult(IEnumerable<T> items)
        {
            Items = new List<T>(items);
        }

        public PageResult(PaginationData pagination)
        {
            Pagination = pagination;
        }

        public PageResult(PaginationData pagination, IEnumerable<T> items)
        {
            Pagination = pagination;
            Items = new List<T>(items);
        }
    }
}
