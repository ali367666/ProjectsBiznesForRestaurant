using AlisRestaurant.Data.Context;
using AlisRestaurant.Data.Entities.HR;
using AlisRestaurant.DTOs.HRDto.Department;
using AlisRestaurant.DTOs.HRDto.Employee;
using AlisRestaurant.DTOs.HRDto.EmployeePosition;
using AlisRestaurant.DTOs.HRDto.Position;
using AlisRestaurant.Validations.EmployeeValidation;
using AlisRestaurant.Validations.HRValidations.DepartmentValidation;
using AlisRestaurant.Validations.HRValidations.PositionValidation;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Identity.Client;


namespace AlisRestaurant.Services.HrService.EmployeePositionServices;

public class CreateEmployeePositionServices
{
    private readonly AppDbContext _context;
    public CreateEmployeePositionServices()
    {
        _context = new AppDbContext();
    }

    public void Execute()
    {
        Console.Clear();

        Console.WriteLine("İşçi üçün yeni vəzifə yarat");
        Console.WriteLine("İşçi ID-si əlavə edin: ");
        var input = Console.ReadLine();

        if (!int.TryParse(input, out int employeeId) || employeeId <= 0)
        {
            Console.WriteLine("Düzgün Employee ID daxil edin!");
            return;
        }

        Console.WriteLine("Vəzifəni əlavə et");
        var inputposition= Console.ReadLine();     
        if(!int.TryParse(inputposition,out int positionid) || positionid <= 0)
        {
            Console.WriteLine("Duzguin Position ID daxil edin!");
            return ;
        }

        Console.WriteLine("Baslangic tarixini qeyd edin");


        var dto = new CreateEmployeePositionRequest
        {
            EmployeeId = employeeId,
            PositionId = positionid,
        };

        Console.WriteLine("Employee ID uğurla qəbul edildi.");
    }

}
