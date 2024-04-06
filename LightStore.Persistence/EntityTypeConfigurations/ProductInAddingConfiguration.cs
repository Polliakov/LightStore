using LightStore.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightStore.Persistence.EntityTypeConfigurations
{
    public class ProductInAddingConfiguration : IEntityTypeConfiguration<ProductInAdding>
    {
        public void Configure(EntityTypeBuilder<ProductInAdding> builder)
        {
            builder.HasKey(entity => new { entity.ProductId, entity.ProductsAddingId });
        }
    }
}
