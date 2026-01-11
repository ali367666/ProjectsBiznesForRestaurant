using AlisRestaurant.Services.HrService.DepartmentServices;
using AlisRestaurant.Services.HrService.EmployeeServices;

namespace AlisRestaurant.Menus.HrMenus;

public class EmployeeMenu
{
    private readonly CreateEmployee _createEmployee = new CreateEmployee();
    private readonly DeleteEmployee _deleteEmployee = new DeleteEmployee();
    private readonly UpdateEmployee _updateEmployee = new   UpdateEmployee();
    private readonly ListEmployee _listEmployee = new ListEmployee();
    public void Show()
    {

        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("=== EMPLOYEE MENYU ===");
            Console.WriteLine("1. Employee List");
            Console.WriteLine("2. Add Employee");
            Console.WriteLine("3. Employee dəyiş");
            Console.WriteLine("4. Employee-ləri sil");
            Console.WriteLine("0. Geri");
            Console.Write("Seçiminizi edin: ");
            var choice = Console.ReadLine()?.Trim();
            switch (choice)
            {
                case "1":
                    _listEmployee.Execute();
                    break;
                case "2":
                    _createEmployee.Execute();
                    break;
                case "3":
                    _updateEmployee.Execute();
                    break;
                case "4":
                    _deleteEmployee.Execute();
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
