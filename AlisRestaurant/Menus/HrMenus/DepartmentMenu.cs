using AlisRestaurant.Services.HrService.DepartmentServices;
using System;



namespace AlisRestaurant.Menus.HrMenus;


    public class DepartmentMenu
    {
        private readonly CreateDepartment _createDepartment = new CreateDepartment();
        private readonly DeleteDepartment _deleteDepartment = new DeleteDepartment();
        private readonly UpdateDepartment _updateDepartment = new UpdateDepartment();
        private readonly ListDepartment _listDepartment = new ListDepartment();

        public void Show()
        {
            bool back = false;

            while (!back)
            {
                Console.Clear();
                Console.WriteLine("=== Department MENYU ===");
                Console.WriteLine("1. Department əlavə et");
                Console.WriteLine("2. Department sil");
                Console.WriteLine("3. Department dəyiş");
                Console.WriteLine("4. Department-ləri göstər");
                Console.WriteLine("0. Geri");
                Console.Write("Seçiminizi edin: ");

                var choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        _createDepartment.Execute();
                        break;
                case "2":
                    _deleteDepartment.Execute();
                    break;
                case "3":
                    _updateDepartment.Execute();
                    break;
                case "4":
                    _listDepartment.Execute();
                    break;
                case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Yanlış seçim! Yalnız 0-4 arası seçim edə bilərsiniz.");
                        Console.WriteLine("Davam etmək üçün Enter basın...");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
