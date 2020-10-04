using Nudes.Paginator.Core;
using Nudes.Paginator.EfCore;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Linq
{
    public static class PaginatorExtensions
    {
        public static IQueryable<T> PaginateBy<T, T2>(this IQueryable<T> requests, PageRequest pageRequest, Expression<Func<T, T2>> defaultOrdering)
        {
            if (pageRequest.HasSortingData) return PaginateBy<T>(requests, pageRequest);
            pageRequest.SortDirection = SortDirection.Ascending;

            if (defaultOrdering.Body is MemberExpression)
                pageRequest.Field = (defaultOrdering.Body as MemberExpression).Member.Name;
            else
            {
                var operand = (defaultOrdering.Body as UnaryExpression).Operand;
                pageRequest.Field = (operand as MemberExpression).Member.Name;
            }

            return requests.OrderBy(defaultOrdering).Skip(pageRequest.PageSize * (pageRequest.Page - 1)).Take(pageRequest.PageSize);
        }

        public static IQueryable<T> PaginateByDescending<T, T2>(this IQueryable<T> requests, PageRequest pageRequest, Expression<Func<T, T2>> defaultOrdering)
        {
            if (pageRequest.HasSortingData) return PaginateBy<T>(requests, pageRequest);
            pageRequest.SortDirection = SortDirection.Descending;

            return requests.OrderByDescending(defaultOrdering).Skip(pageRequest.PageSize * (pageRequest.Page - 1)).Take(pageRequest.PageSize);
        }

        public static IQueryable<T> PaginateBy<T>(this IQueryable<T> requests, PageRequest pageRequest)
        {
            if (pageRequest?.Field == null) throw new ArgumentNullException($"{nameof(pageRequest)}.{nameof(pageRequest.Field)}");

            if (pageRequest?.SortDirection.HasValue != true) throw new ArgumentNullException($"{nameof(pageRequest)}.{nameof(pageRequest.SortDirection)}");

            try
            {
                var propInfo = typeof(T).GetProperties().Where(d => d.Name.ToLowerInvariant() == pageRequest?.Field?.ToLowerInvariant()).FirstOrDefault();

                if (propInfo == null) throw new InvalidSortingFieldException(pageRequest?.Field);

                ParameterExpression arg = Expression.Parameter(typeof(T), "SortingProperty");
                MemberExpression property = Expression.Property(arg, pageRequest.Field);
                var selector = Expression.Lambda(property, new ParameterExpression[] { arg });

                var methodName = pageRequest.SortDirection.Value == SortDirection.Ascending ? nameof(Enumerable.OrderBy) : nameof(Enumerable.OrderByDescending);

                var method = typeof(Queryable).GetMethods()
                                              .Where(m => m.Name == methodName)
                                              .Where(m => m.GetParameters().Length == 2)
                                              .Single();

                MethodInfo memberInfo = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);

                return ((IOrderedQueryable<T>)memberInfo.Invoke(memberInfo, new object[] { requests, selector }))
                            .Skip(pageRequest.PageSize * (pageRequest.Page - 1)).Take(pageRequest.PageSize);
            }
            catch (Exception ex)
            {
                throw new InvalidSortingFieldException(pageRequest.Field, ex);
            }
        }
    }
}
