using AlisRestaurant.Data.Entities;
using AlisRestaurant.Data.Entities.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlisRestaurant.Configuration.CompanyConfiguration;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable(nameof(Company));
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(c => c.Address)
            .IsRequired()
            .HasMaxLength(300);
        builder.Property(c => c.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);
        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(100);
        builder.HasMany(c => c.Restaurants)
               .WithOne(r => r.Company)
               .HasForeignKey(r => r.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
