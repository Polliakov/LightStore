using LightStore.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightStore.Persistence.EntityTypeConfigurations
{
    public class ProductsAddingConfiguration : IEntityTypeConfiguration<ProductsAdding>
    {
        public void Configure(EntityTypeBuilder<ProductsAdding> builder)
        {
            builder.HasKey(entity => entity.Id);
        }
    }
}
