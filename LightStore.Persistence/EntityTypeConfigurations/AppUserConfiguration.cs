using LightStore.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightStore.Persistence.EntityTypeConfigurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(entity => entity.AppUserId);
            builder.HasIndex(entity => entity.Email);
            builder.Property(entity => entity.Email).HasMaxLength(254).IsRequired();
            builder.Property(entity => entity.Password).HasMaxLength(72).IsRequired();
        }
    }
}
