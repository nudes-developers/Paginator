using System.Collections.Generic;

namespace Nudes.Paginator.Core;

/// <summary>
/// Page request, information how the request should be paginated, should be extracted from queryParams
/// If you are using 
/// </summary>
public class PageRequest
{
    /// <summary>
    /// Default PageSize
    /// </summary>
    public static int DefaultPageSize { get; set; } = 30;

    /// <summary>
    /// Page, starts at 1
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Page size, can be overriden by changing <code>PageRequest.DefaultPageSize </code>
    /// </summary>
    public int PageSize { get; set; } = DefaultPageSize;

    /// <summary>
    /// Enumerable of sorting definition, can be specified more than one field in sequence each with its own direction
    /// </summary>
    public IEnumerable<SortingDefinition> Sorting { get; set; }

    /// <summary>
    /// Creates a result based on this request and other information
    /// </summary>
    /// <typeparam name="T">Type of the items</typeparam>
    /// <param name="items">Items</param>
    /// <param name="total">Total ammount of items (not only the paginated)</param>
    /// <returns>PageResult of items</returns>
    public PageResult<T> ToResult<T>(IEnumerable<T> items, int total) 
        => new(this, total, items);
}
