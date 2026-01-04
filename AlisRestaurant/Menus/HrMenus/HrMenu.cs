namespace AlisRestaurant.Menus.HrMenus;

public class HrMenu
{
   
        public void Show()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== HR ƏSAS MENYU ===");
                Console.WriteLine("1. Department");
                Console.WriteLine("2. Employee");
                Console.WriteLine("3. Position");
                Console.WriteLine("4. EmployeePosition");
                Console.WriteLine("0. Geri / Çıxış");
                Console.Write("Seçiminizi edin: ");

                var choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        var departmentMenu = new DepartmentMenu();
                        departmentMenu.Show(); // Department submenu açılır
                        break;
                    case "2":
                        var employeeMenu = new EmployeeMenu();
                        employeeMenu.Show(); // Employee submenu açılır
                    break;
                    case "3":
                        var positionMenu = new PositionMenu();
                        positionMenu.Show(); // Position submenu açılır
                        break;
                    case "4":
                        // EmployeePositionMenu çağır
                        break;
                    case "0":
                        exit = true;
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



    
