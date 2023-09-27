using System;
using System.Linq;

namespace Nudes.Paginator.Core;

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
    /// Sequence of sorting definitions in which data has been sorted into
    /// </summary>
    public SortingDefinition[] Sorting { get; set; }

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

    /// <summary>
    /// Creates a pagination data with empty information
    /// </summary>
    public PaginationData()
    { /* as is */ }


    /// <summary>
    /// Creates a pagination data based on the request
    /// </summary>
    /// <param name="request">request that was used to create this pagination</param>
    /// <param name="total">total of items on this page</param>
    public PaginationData(PageRequest request, long total)
    {
        Page = request.Page;
        PageSize = request.PageSize;
        Sorting = request.Sorting?.ToArray();
        Total = total;
    }
}
