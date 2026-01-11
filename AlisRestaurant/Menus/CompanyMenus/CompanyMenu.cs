using AlisRestaurant.Services.CompanyService;
using AlisRestaurant.Services.HrService.DepartmentServices;
using System;

namespace AlisRestaurant.Menus.CompanyMenus;

public class CompanyMenu
{
    private readonly CreateCompany _createCompany=new CreateCompany();
    private readonly ListCompany _listCompany=new ListCompany();
    private readonly UpdateCompany _updateCompany=new UpdateCompany();
    private readonly DeleteCompany _deleteCompany=new DeleteCompany();
    public void Show()
    {
        bool back = false;

        while (!back)
        {
            Console.Clear();
            Console.WriteLine("=== Company MENYU ===");
            Console.WriteLine("1. Company əlavə et");
            Console.WriteLine("2. Company sil");
            Console.WriteLine("3. Company dəyiş");
            Console.WriteLine("4. Company-ləri göstər");
            Console.WriteLine("0. Geri");
            Console.Write("Seçiminizi edin: ");

            var choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1":
                    _createCompany.Execute();
                    break;
                case "2":
                    _deleteCompany.Execute();
                    break;
                case "3":
                    _updateCompany.Execute();
                    break;
                case "4":
                    _listCompany.Execute();
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
