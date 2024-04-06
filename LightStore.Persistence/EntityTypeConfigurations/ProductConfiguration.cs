using LightStore.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightStore.Persistence.EntityTypeConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder
                .HasOne(product => product.ActualPrice)
                .WithOne(price => price.Product)
                .HasForeignKey<Product>(p => p.ActualPriceId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Property(entity => entity.Title).HasMaxLength(128).IsRequired();
            builder.Property(entity => entity.Size).HasMaxLength(64);
        }
    }
}
