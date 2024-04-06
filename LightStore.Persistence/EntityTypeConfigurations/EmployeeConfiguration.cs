using LightStore.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightStore.Persistence.EntityTypeConfigurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(entity => entity.AppUserId);
            builder.Property(entity => entity.Name).HasMaxLength(128).IsRequired();
            builder.Property(entity => entity.Surname).HasMaxLength(128).IsRequired();
            builder.Property(entity => entity.Patronymic).HasMaxLength(128);
        }
    }
}
