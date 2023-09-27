using Nudes.Paginator.Core;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Linq;

/// <summary>
/// Extensions for pagination
/// </summary>
public static class PaginatorExtensions
{
    /// <summary>
    /// Paginates an IQueryable based on page, page size and enum of sortings information
    /// </summary>
    /// <typeparam name="T">Type of the data in the datasource</typeparam>
    /// <param name="query">The datasource that will be paginated</param>
    /// <param name="page">The page first is 1</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="sortings">Sortings information</param>
    /// <returns>An paginated Queryable</returns>
    public static IQueryable<T> PaginateBy<T>(this IQueryable<T> query, int page, int pageSize, SortingDefinition[] sortings)
    {
        if (sortings?.Any() == true)
        {
            int number = 0;

            foreach (var sort in sortings)
            {
                ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "SortingProperty");
                Expression property = parameterExpression;
                foreach (var part in sort.Field.Split('.'))
                {
                    try
                    {
                        property = Expression.PropertyOrField(property, part);
                    }
                    catch (ArgumentException)
                    {
                        throw new InvalidSortingException(part);
                    }
                }

                LambdaExpression lambdaExpression = Expression.Lambda(property, parameterExpression);

                string methodName = ((sort.SortDirection == SortDirection.Ascending) ?
                    number == 0 ? nameof(Queryable.OrderBy) : nameof(Queryable.ThenBy) :
                    number == 0 ? nameof(Queryable.OrderByDescending) : nameof(Queryable.ThenByDescending));

                MethodInfo methodInfo = typeof(Queryable).GetMethods()
                    .Where(d => d.Name == methodName)
                    .Where(d => d.GetParameters().Length == 2)
                    .Single()
                    .MakeGenericMethod(typeof(T), property.Type);

                query = (IQueryable<T>)methodInfo.Invoke(methodInfo, new object[2] { query, lambdaExpression });
                number++;
            }
        }
        return query
            .Skip(pageSize * (page - 1))
            .Take(pageSize);
    }

    /// <summary>
    /// Paginates an IQueryable based on the PageRequest and a default sorting order
    /// </summary>
    /// <typeparam name="T">Type of the data in the datasource</typeparam>
    /// <typeparam name="T2">Type of the member that is being sort by default</typeparam>
    /// <param name="requests">IQueryable that will be paginated</param>
    /// <param name="pageRequest">Pagination Request</param>
    /// <param name="defaultOrdering">Default sorting order when Page Request has no sorting data</param>
    /// <returns>An paginated Queryable</returns>
    public static IQueryable<T> PaginateBy<T, T2>(this IQueryable<T> requests, PageRequest pageRequest, Expression<Func<T, T2>> defaultOrdering)
    {
        if (pageRequest.Sorting?.Any() == true)
            return PaginateBy<T>(requests, pageRequest.Page, pageRequest.PageSize, pageRequest.Sorting.ToArray());

        return requests.OrderBy(defaultOrdering)
            .Skip(pageRequest.PageSize * (pageRequest.Page - 1))
            .Take(pageRequest.PageSize);
    }

    /// <summary>
    /// Paginates an IQueryable based on the PageRequest and a default sorting order in descending format
    /// </summary>
    /// <typeparam name="T">Type of the data in the datasource</typeparam>
    /// <typeparam name="T2">Type of the member that is being sort by default</typeparam>
    /// <param name="requests">IQueryable that will be paginated</param>
    /// <param name="pageRequest">Pagination Request</param>
    /// <param name="defaultOrdering">Default sorting order when Page Request has no sorting data</param>
    /// <returns>An paginated Queryable</returns>
    public static IQueryable<T> PaginateByDescending<T, T2>(this IQueryable<T> requests, PageRequest pageRequest, Expression<Func<T, T2>> defaultOrdering)
    {
        if (pageRequest.Sorting?.Any() == true) 
            return PaginateBy<T>(requests, pageRequest.Page, pageRequest.PageSize, pageRequest.Sorting.ToArray());

        return requests.OrderByDescending(defaultOrdering)
            .Skip(pageRequest.PageSize * (pageRequest.Page - 1))
            .Take(pageRequest.PageSize);
    }
}
