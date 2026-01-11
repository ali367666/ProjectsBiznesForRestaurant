using AlisRestaurant.Data.Context;

namespace AlisRestaurant.Services.RestaurantService;

public class UpdateRestaurant
{
    private readonly AppDbContext _dbContext;
    public UpdateRestaurant()
    {
        _dbContext = new AppDbContext();
    }
    public void Execute()
    {
        Console.Clear();
        Console.WriteLine("Restoran Yeniləmə");
        Console.Write("Yenilənəcək restoranın ID-sini daxil edin: ");
        if (!int.TryParse(Console.ReadLine(), out int restaurantId))
        {
            Console.WriteLine("Yanlış ID formatı! Davam etmək üçün Enter basın...");
            Console.ReadLine();
            return;
        }
        var restaurant = _dbContext.Restaurants.Find(restaurantId);
        if (restaurant == null)
        {
            Console.WriteLine("Restoran tapılmadı! Davam etmək üçün Enter basın...");
            Console.ReadLine();
            return;
        }
        Console.Write("Yeni restoran adı (mövcud: {0}): ", restaurant.Name);
        var name = Console.ReadLine();
        Console.Write("Yeni restoran ünvanı (mövcud: {0}): ", restaurant.Address);
        var address = Console.ReadLine();
        Console.Write("Yeni restoran telefon nömrəsi (mövcud: {0}): ", restaurant.PhoneNumber);
        var phoneNumber = Console.ReadLine();
        Console.Write("Yeni restoran emaili (mövcud: {0}): ", restaurant.Email);
        var email = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(name)) restaurant.Name = name;
        if (!string.IsNullOrWhiteSpace(address)) restaurant.Address = address;
        if (!string.IsNullOrWhiteSpace(phoneNumber)) restaurant.PhoneNumber = phoneNumber;
        if (!string.IsNullOrWhiteSpace(email)) restaurant.Email = email;
        _dbContext.SaveChanges();
        Console.WriteLine("Restoran uğurla yeniləndi! Davam etmək üçün Enter basın...");
        Console.ReadLine();
    }
}
