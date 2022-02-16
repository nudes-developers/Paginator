using Nudes.Retornator.Core;
using System.Collections.Generic;

namespace Nudes.Paginator.Core
{
    /// <summary>
    /// Base class to all paginated result
    /// </summary>
    /// <typeparam name="T">Type of paginated items</typeparam>
    public class PageResult<T> 
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

        public PageResult(PaginationData pagination, IEnumerable<T> items) : this(pagination)
        {
            Items = new List<T>(items);
        }

        public PageResult(PageRequest request, long total, IEnumerable<T> items) : this(new PaginationData(request, total), items)
        {
            /*as is*/
        }
    }
}
