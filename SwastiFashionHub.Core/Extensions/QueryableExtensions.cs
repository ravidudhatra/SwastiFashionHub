using Microsoft.EntityFrameworkCore;
using SwastiFashionHub.Core.Exceptions;
using SwastiFashionHub.Core.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Core.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize, string orderBy) where T : class
        {
            if (source == null)
                throw new CustomException("Empty");

            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 10 : pageSize;
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;

            int count = await source.AsNoTracking().CountAsync();

            if (!string.IsNullOrWhiteSpace(orderBy))
                source = ApplyOrderBy<T>(source, orderBy);

            List<T> items = await source.Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();

            return PaginatedResult<T>.Success(items, count, pageNumber, pageSize);
        }

        private static IQueryable<T> ApplyOrderBy<T>(IQueryable<T> items, string orderBy) where T : class
        {
            // Split the orderBy parameter to get the property name and sort direction
            string[] orderByParts = orderBy.Split(' ');

            string propertyName = orderByParts[0];
            bool isDescending = orderByParts.Length > 1 && orderByParts[1].Equals("desc", StringComparison.OrdinalIgnoreCase);

            // Use reflection to get the property info
            PropertyInfo propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null)
                throw new ArgumentException($"Invalid orderBy property name: {propertyName}");

            // Create the expression tree for the property access
            ParameterExpression parameterExp = Expression.Parameter(typeof(T));
            MemberExpression propertyExp = Expression.Property(parameterExp, propertyInfo);
            LambdaExpression orderByExp = Expression.Lambda(propertyExp, parameterExp);

            // Create the ordering method based on the sort direction
            string orderByMethod = isDescending ? "OrderByDescending" : "OrderBy";
            MethodCallExpression orderByCallExp = Expression.Call(typeof(Queryable),
                orderByMethod,
                new[] { typeof(T), propertyInfo.PropertyType },
                items.Expression,
                Expression.Quote(orderByExp));

            // Apply the ordering
            return (IQueryable<T>)items.Provider.CreateQuery(orderByCallExp);
        }
    }
}
