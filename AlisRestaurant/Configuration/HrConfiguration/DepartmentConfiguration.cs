using AlisRestaurant.Data.Entities.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FluentValidation.Internal;

namespace AlisRestaurant.Configuration.HrConfiguration;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable(nameof(Department));
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(d => d.Name)
            .IsUnique();

        builder.HasMany(d => d.Positions)
               .WithOne(p => p.Department)
               .HasForeignKey(p => p.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict);//bele etme sebebim Department silindiyse ona aid positionlarida silmesin
    }
}
