using AlisRestaurant.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlisRestaurant.Configuration.RestaurantConfiguration;

public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.ToTable(nameof(Restaurant));
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(r => r.Address)
            .IsRequired()
            .HasMaxLength(300);
        builder.Property(r => r.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);
        builder.Property(r => r.Email)
            .IsRequired()
            .HasMaxLength(100);
        builder.HasOne(r => r.Company)
               .WithMany(c => c.Restaurants)
               .HasForeignKey(r => r.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
