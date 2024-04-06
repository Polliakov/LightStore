using LightStore.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightStore.Persistence.EntityTypeConfigurations
{
    public class ProductsCategoryConfiguration : IEntityTypeConfiguration<ProductsCategory>
    {
        public void Configure(EntityTypeBuilder<ProductsCategory> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Name).IsRequired();
        }
    }
}
