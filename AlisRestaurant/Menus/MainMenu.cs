using AlisRestaurant.Menus.CompanyMenus;
using AlisRestaurant.Menus.HrMenus;
using AlisRestaurant.Menus.RestaurantMenus;

namespace AlisRestaurant.Menus;


    public class MainMenu
    {
        public void Show()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("===== ALIS RESTAURANT SİSTEMİ =====");
                Console.WriteLine("1. Şirkət");
                Console.WriteLine("2. HR");
                Console.WriteLine("3. Restoranlar");
                Console.WriteLine("4. Menu");
                Console.WriteLine("0. Çıxış");
                Console.Write("Seçiminizi edin: ");

                var choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        new CompanyMenu().Show();
                        break;

                    case "2":
                        new HrMenu().Show();
                        break;

                    case "3":
                        new RestaurantMenu().Show();
                        break;

                    //case "4":
                    //    new FoodMenu().Show();
                    //    break;

                    case "0":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Yanlış seçim!");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }

