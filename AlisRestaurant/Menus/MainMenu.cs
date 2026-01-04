using AlisRestaurant.Menus.HrMenus;

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
                Console.WriteLine("1. HR");
                Console.WriteLine("2. Depo / Stok");
                Console.WriteLine("3. Rezervasiya");
                Console.WriteLine("4. Menu");
                Console.WriteLine("0. Çıxış");
                Console.Write("Seçiminizi edin: ");

                var choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        new HrMenu().Show();
                        break;

                    //case "2":
                    //    new StockMenu().Show();
                    //    break;

                    //case "3":
                    //    new ReservationMenu().Show();
                    //    break;

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

