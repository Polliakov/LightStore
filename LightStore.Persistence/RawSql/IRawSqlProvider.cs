using System;
using System.Collections.Generic;

namespace LightStore.Persistence.RawSql
{
    public interface IRawSqlProvider
    {
        string MoveProductsToCategory(IEnumerable<Guid> from, Guid? to);
        string MoveProductsToCategory(Guid? from, Guid? to);
    }
}
