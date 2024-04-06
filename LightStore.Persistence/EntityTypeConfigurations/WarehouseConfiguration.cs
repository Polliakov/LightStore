using LightStore.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightStore.Persistence.EntityTypeConfigurations
{
    public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Name).HasMaxLength(64).IsRequired();
            builder.Property(entity => entity.Address).HasMaxLength(128).IsRequired();
        }
    }
}
