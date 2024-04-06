using LightStore.Application.Dtos.Pagination;
using System;
using System.Linq;

namespace LightStore.Application.Utils
{
    public static class PaginationExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, PaginationArgs pagination)
        {
            if (pagination.Page < 1)
                throw new ArgumentException("Pages starts with 1.", nameof(pagination.Page));
            if (pagination.PageSize < 1)
                throw new ArgumentException("Pages size starts with 1.", nameof(pagination.PageSize));

            var from = checked((pagination.Page - 1) * pagination.PageSize);
            return query.Skip(from).Take(pagination.PageSize);
        }
    }
}
