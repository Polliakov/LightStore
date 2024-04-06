using LightStore.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightStore.Persistence.EntityTypeConfigurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(entity => entity.AppUserId);
            builder.Property(entity => entity.Name).HasMaxLength(128).IsRequired();
            builder.Property(entity => entity.Surname).HasMaxLength(128).IsRequired();
            builder.Property(entity => entity.Patronymic).HasMaxLength(128);
        }
    }
}
