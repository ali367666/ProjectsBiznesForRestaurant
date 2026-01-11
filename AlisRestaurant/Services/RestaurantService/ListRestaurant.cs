using AlisRestaurant.Data.Context;

namespace AlisRestaurant.Services.RestaurantService;

public class ListRestaurant
{
    private readonly AppDbContext _dbContext;
    public ListRestaurant()
    {
        _dbContext = new AppDbContext();
    }
    public void Execute()
    {
        Console.Clear();
        Console.WriteLine("=== Restoranlarin Siyahisi ===\n");
        var restaurants = _dbContext.Restaurants.ToList();
        if (!restaurants.Any())
        {
            Console.WriteLine("Heç bir restoran tapılmadı. Davam etmək üçün Enter basın...");
            Console.ReadLine();
            return;
        }
        foreach (var restaurant in restaurants)
        {
            Console.WriteLine($"ID: {restaurant.Id}");
            Console.WriteLine($"Ad: {restaurant.Name}");
            Console.WriteLine($"Ünvan: {restaurant.Address}");
            Console.WriteLine($"Telefon Nömrəsi: {restaurant.PhoneNumber}");
            Console.WriteLine($"Email: {restaurant.Email}");
            Console.WriteLine(new string('-', 30));
        }
        Console.WriteLine("Davam etmək üçün Enter basın...");
        Console.ReadLine();
    }
}
