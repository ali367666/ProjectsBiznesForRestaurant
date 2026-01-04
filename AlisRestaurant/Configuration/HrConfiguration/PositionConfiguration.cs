using AlisRestaurant.Data.Entities.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FluentValidation.Internal;

namespace AlisRestaurant.Configuration.HrConfiguration;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable(nameof(Position));
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(p => p.Name)
               .IsUnique();

        builder.HasOne(p => p.Department)         
               .WithMany(d => d.Positions)         
               .HasForeignKey(p => p.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict); // Department silinməsin, əgər Position varsa
    }
}
