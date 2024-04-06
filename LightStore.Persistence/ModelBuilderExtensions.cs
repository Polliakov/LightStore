using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LightStore.Persistence
{
    public static class ModelBuilderExtensions
    {
        public static void ApplySoftDeleteFilters(this ModelBuilder builder,
                                                  Type interfaceType,
                                                  string propertyName = "Deleted",
                                                  object notDeletedValue = null)
        {
            if (!interfaceType.IsInterface)
                throw new ArgumentException($"Type of parameter \"{nameof(interfaceType)}\" must be an interface.");

            foreach (var entityType in builder.Model.GetEntityTypes()
                .Where(et => et.ClrType.GetInterface(interfaceType.Name) is not null))
            {
                var entityParam = Expression.Parameter(entityType.ClrType);
                entityType.SetQueryFilter(
                    Expression.Lambda(
                        Expression.Equal(
                            Expression.Property(entityParam, propertyName),
                            Expression.Constant(notDeletedValue)
                        ),
                        entityParam
                    )
                );
            }
        }
    }
}
