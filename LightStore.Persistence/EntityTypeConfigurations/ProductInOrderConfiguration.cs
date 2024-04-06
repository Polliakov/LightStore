using LightStore.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightStore.Persistence.EntityTypeConfigurations
{
    public class ProductInOrderConfiguration : IEntityTypeConfiguration<ProductInOrder>
    {
        public void Configure(EntityTypeBuilder<ProductInOrder> builder)
        {
            builder.HasKey(entity => new { entity.ProductId, entity.OrderId });
            builder
                .HasOne(pio => pio.Product)
                .WithMany(product => product.ProductInOrders)
                .HasForeignKey(pio => pio.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
