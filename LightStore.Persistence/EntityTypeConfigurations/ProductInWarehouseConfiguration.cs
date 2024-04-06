using LightStore.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightStore.Persistence.EntityTypeConfigurations
{
    public class ProductInWarehouseConfiguration : IEntityTypeConfiguration<ProductInWarehouse>
    {
        public void Configure(EntityTypeBuilder<ProductInWarehouse> builder)
        {
            builder.HasKey(entity => new { entity.ProductId, entity.WarehouseId });
        }
    }
}
