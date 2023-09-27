using System.Collections.Generic;
using System.Linq;

namespace Nudes.Paginator.Core;

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
    /// Items that are shown in the specified page
    /// </summary>
    public virtual IEnumerable<T> Items { get; set; }

    /// <summary>
    /// Creates a page result with empty items and default pagination data
    /// </summary>
    public PageResult()
    {
        Pagination = new PaginationData();
        Items = Enumerable.Empty<T>();
    }

    /// <summary>
    /// Creates a PageResult with the items specified
    /// </summary>
    /// <param name="items">items of the page</param>
    public PageResult(IEnumerable<T> items)
    {
        Items = items;
    }

    /// <summary>
    /// Creates a PageResult based on the pagination data, with empty items
    /// </summary>
    /// <param name="pagination">pagination data</param>
    public PageResult(PaginationData pagination)
    {
        Pagination = pagination;
        Items = Enumerable.Empty<T>();
    }

    /// <summary>
    /// Creates a PageResult based on the pagination data and the items
    /// </summary>
    /// <param name="pagination">pagination data</param>
    /// <param name="items">items of the page</param>
    public PageResult(PaginationData pagination, IEnumerable<T> items) : this(pagination)
    {
        Items = items;
    }

    /// <summary>
    /// Creates a PageResult based on the pagination request, total items and the items
    /// </summary>
    /// <param name="request">pagination request</param>
    /// <param name="total">total of items</param>
    /// <param name="items">items of the page</param>
    public PageResult(PageRequest request, long total, IEnumerable<T> items) : this(new PaginationData(request, total), items)
    {
        /* as is */
    }
}
