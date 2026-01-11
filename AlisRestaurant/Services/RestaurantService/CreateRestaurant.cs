using AlisRestaurant.Data.Context;
using AlisRestaurant.Data.Entities;

namespace AlisRestaurant.Services.RestaurantService;

public class CreateRestaurant
{
    private readonly AppDbContext _dbContext;
    public CreateRestaurant()
    {
        _dbContext = new AppDbContext();
    }
    public void Execute()
    {
        Console.Clear();
        Console.WriteLine("Yeni restoran əlavə edin");
        Console.Write("Restoran adı: ");
        var name = Console.ReadLine()!;
        Console.Write("Restoran ünvanı: ");
        var address = Console.ReadLine()!;
        Console.Write("Restoran telefon nömrəsi: ");
        var phoneNumber = Console.ReadLine()!;
        Console.Write("Restoran emaili: ");
        var email = Console.ReadLine()!;
        Console.WriteLine("Company ID daxil et");
        var companyIdInput = Console.ReadLine()!;
        var restaurant = new Restaurant
        {
            Name = name,
            Address = address,
            PhoneNumber = phoneNumber,
            Email = email,
            CompanyId = int.Parse(companyIdInput)
        };
        _dbContext.Restaurants.Add(restaurant);
        _dbContext.SaveChanges();
        Console.WriteLine("\nRestoran uğurla əlavə olundu!");
        Console.WriteLine("Davam etmək üçün Enter basın...");
        Console.ReadLine();
    }
}
