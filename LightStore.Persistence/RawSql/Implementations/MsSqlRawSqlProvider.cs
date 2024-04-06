using LightStore.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightStore.Persistence.RawSql.Implementations
{
    public class MsSqlRawSqlProvider : IRawSqlProvider
    {
        public string MoveProductsToCategory(IEnumerable<Guid> from, Guid? to)
        {
            var fromParam = string.Join(',', from.Select(g => $"'{g}'"));
            var toParam = to.HasValue ? $"'{to}'" : "NULL";

            return $"UPDATE {nameof(Product)}s SET {nameof(Product.CategoryId)} = {toParam} " +
                   $"WHERE {nameof(Product.CategoryId)} IN ({fromParam})";
        }

        public string MoveProductsToCategory(Guid? from, Guid? to)
        {
            var fromParam = from.HasValue ? $"= '{from.Value}'" : "IS NULL";
            var toParam = to.HasValue ? $"'{to}'" : "NULL";

            return $"UPDATE {nameof(Product)}s SET {nameof(Product.CategoryId)} = {toParam} " +
                   $"WHERE {nameof(Product.CategoryId)} {fromParam}";
        }
    }
}
