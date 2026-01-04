using AlisRestaurant.Data.Entities.HR;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FluentValidation.Internal;
//using System.Data;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
   
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable(nameof(Employee));

        builder.HasKey(e => e.Id);

        builder.Property(e => e.FullName)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(e => e.Phone)
               .IsRequired()
               .HasMaxLength(15);

        builder.HasIndex(e => e.Phone)
               .IsUnique();
        builder.Property(e => e.Email)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasIndex(e => e.Email)
               .IsUnique();

        builder.Property(e => e.HireDate);

        

        builder.Property(e => e.IsActive)
               .HasDefaultValue(true);

        builder.Property(e => e.BirthDate);
      

      
        builder.Property(e => e.FinCode)
               .IsRequired()
               .HasMaxLength(7);
        builder.HasIndex(e => e.FinCode)
               .IsUnique();

    }
}
