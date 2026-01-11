using AlisRestaurant.Services.RestaurantService;

namespace AlisRestaurant.Menus.RestaurantMenus;

public class RestaurantMenu
{
    private readonly CreateRestaurant _createRestaurant = new CreateRestaurant();
    private readonly ListRestaurant _listRestaurants = new ListRestaurant();
    private readonly UpdateRestaurant _updateRestaurant = new UpdateRestaurant();
    private readonly DeleteRestaurant _deleteRestaurant = new DeleteRestaurant();

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Restoran Menyusu");
            Console.WriteLine("1. Yeni restoran əlavə et");
            Console.WriteLine("2. Restoranları siyahıla");
            Console.WriteLine("3. Restoranı yenilə");
            Console.WriteLine("4. Restoranı sil");
            Console.WriteLine("0. Geri qayıt");
            Console.Write("Seçiminizi edin: ");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    _createRestaurant.Execute();
                    break;
                case "2":
                    _listRestaurants.Execute();
                    break;
                case "3":
                    _updateRestaurant.Execute();
                    break;
                case "4":
                    _deleteRestaurant.Execute();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Yanlış seçim! Yenidən cəhd edin.");
                    Console.WriteLine("Davam etmək üçün Enter basın...");
                    Console.ReadLine();
                    break;
            }
        }
    }
}
