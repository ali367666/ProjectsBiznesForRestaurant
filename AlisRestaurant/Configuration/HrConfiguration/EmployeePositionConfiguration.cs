namespace AlisRestaurant.Configuration.HrConfiguration;
using AlisRestaurant.Data.Entities.HR;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FluentValidation.Internal;

public class EmployeePositionConfiguration : IEntityTypeConfiguration<EmployeePosition>
{
    public void Configure(EntityTypeBuilder<EmployeePosition> builder)
    {
        builder.ToTable(nameof(EmployeePosition));
        builder.HasKey(ep => ep.Id);

        builder.Property(e => e.EmployeeId)
               .IsRequired();
        builder.Property(e => e.PositionId)
               .IsRequired();
        builder.Property(e => e.AssignedDate);

        

        builder.HasOne(ep => ep.Employee)
               .WithMany(e => e.EmployeePositions) 
               .HasForeignKey(ep => ep.EmployeeId)
               .OnDelete(DeleteBehavior.Restrict); 

        
        builder.HasOne(ep => ep.Position)
               .WithMany(p => p.EmployeePositions) 
               .HasForeignKey(ep => ep.PositionId)
               .OnDelete(DeleteBehavior.Restrict); 



    }
}
