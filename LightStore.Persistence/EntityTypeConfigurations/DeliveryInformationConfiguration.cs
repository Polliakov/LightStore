using LightStore.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightStore.Persistence.EntityTypeConfigurations
{
    public class DeliveryInformationConfiguration : IEntityTypeConfiguration<DeliveryInformation>
    {
        public void Configure(EntityTypeBuilder<DeliveryInformation> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Price).HasPrecision(12, 2);
            builder.Property(entity => entity.Address).HasMaxLength(128).IsRequired();
        }
    }
}
