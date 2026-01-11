using AlisRestaurant.Data.Entities;
using AlisRestaurant.Data.Entities.HR;
using Microsoft.EntityFrameworkCore;

namespace AlisRestaurant.Data.Context;

public class AppDbContext:DbContext
{
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-ET5TVFM\SQLEXPRESS;
        Initial Catalog=RestaurantProjects;
        Integrated Security=True;
        TrustServerCertificate=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<EmployeePosition> EmployeePositions { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }

}
