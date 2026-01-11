using AlisRestaurant.Data.Context;

namespace AlisRestaurant.Services.RestaurantService;

public class DeleteRestaurant
{
    private readonly AppDbContext _dbContext;
    public DeleteRestaurant()
    {
        _dbContext = new AppDbContext();
    }
    public void Execute()
    {
       Console.Clear();
       Console.WriteLine("Restoran Silmə");
       Console.Write("Silinəcək restoranın ID-sini daxil edin: ");
       if (!int.TryParse(Console.ReadLine(), out int restaurantId))
       {
           Console.WriteLine("Yanlış ID formatı! Davam etmək üçün Enter basın...");
           Console.ReadLine();
           return;
       }
       using var context = new AppDbContext();
       var restaurant = context.Restaurants.Find(restaurantId);
       if (restaurant == null)
       {
           Console.WriteLine("Restoran tapılmadı! Davam etmək üçün Enter basın...");
           Console.ReadLine();
           return;
       }
       context.Restaurants.Remove(restaurant);
       context.SaveChanges();
       Console.WriteLine("Restoran uğurla silindi! Davam etmək üçün Enter basın...");
       Console.ReadLine();
    }
}
