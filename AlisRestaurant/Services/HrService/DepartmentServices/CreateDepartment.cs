using AlisRestaurant.Data.Context;
using AlisRestaurant.Data.Entities.HR;
using AlisRestaurant.DTOs.HRDto.Department;
using AlisRestaurant.Validations.HRValidations.DepartmentValidation;
using AlisRestaurant.Validations;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using FluentValidation.Results;

namespace AlisRestaurant.Services.HrService.DepartmentServices;

public class CreateDepartment
{
    private readonly AppDbContext _dbContext;

    public CreateDepartment()
    {
        _dbContext = new AppDbContext();
    }

    public void Execute()
    {
        Console.Clear();
        Console.WriteLine("=== Yeni Department Yarat ===");

        // 1️⃣ User input
        Console.Write("Department adı daxil edin: ");
        var name = Console.ReadLine();

        // 2️⃣ DTO doldur
        var dto = new CreateDepartmentRequest { Name = name! };

        // 3️⃣ Validator çağır
        var validator = new CreateDepartmentValidation(_dbContext);
        var result = validator.Validate(dto);

        if (!result.IsValid)
        {
            Console.WriteLine("\nXətalar:");
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"- {error.ErrorMessage}");
            }
            Console.WriteLine("Davam etmək üçün Enter basın...");
            Console.ReadLine();
            return;
        }

        // 4️⃣ DB əməliyyatı
        var department = new Department { Name = dto.Name };
        _dbContext.Departments.Add(department);
        _dbContext.SaveChanges();

        Console.WriteLine("\nDepartment uğurla əlavə olundu!");
        Console.WriteLine("Davam etmək üçün Enter basın...");
        Console.ReadLine();
    }
}
