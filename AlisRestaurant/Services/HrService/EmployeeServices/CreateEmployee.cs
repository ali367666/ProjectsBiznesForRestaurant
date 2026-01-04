using AlisRestaurant.Data.Context;
using AlisRestaurant.Data.Entities.HR;
using AlisRestaurant.DTOs.HRDto.Department;
using AlisRestaurant.DTOs.HRDto.Employee;
using AlisRestaurant.DTOs.HRDto.Position;
using AlisRestaurant.Validations.EmployeeValidation;
using AlisRestaurant.Validations.HRValidations.DepartmentValidation;
using AlisRestaurant.Validations.HRValidations.PositionValidation;

namespace AlisRestaurant.Services.HrService.EmployeeServices;

public class CreateEmployee
{
    private readonly AppDbContext _context;

    public CreateEmployee()
    {
        _context = new AppDbContext();
    }

    public void Execute()
    {
        Console.Clear();
        Console.WriteLine("=== Yeni Employee Yarat ===");

        // Ad və soyad
        Console.Write("Employee adı daxil edin: ");
        var firstName = Console.ReadLine()!;
        Console.Write("Employee soyadı daxil edin: ");
        var lastName = Console.ReadLine()!;
        var fullName = string.Join(" ", new[] { firstName, lastName }.Where(s => !string.IsNullOrWhiteSpace(s)));

        // Email
        Console.Write("Employee email daxil edin: ");
        var email = Console.ReadLine()!;

        // Telefon
        Console.Write("Employee telefon nömrəsi daxil edin: ");
        var phone = Console.ReadLine()!;

        // FinCode
        Console.Write("Employee FinCode daxil edin: ");
        var finCode = Console.ReadLine()!;

        // Doğum tarixi
        Console.Write("Employee doğum tarixi daxil edin (YYYY-MM-DD): ");
        var birthDateInput = Console.ReadLine()!;
        if (!DateOnly.TryParse(birthDateInput, out DateOnly birthDate))
        {
            Console.WriteLine("Doğum tarixi düzgün deyil");
            Console.ReadLine();
            return;
        }

        DateTime hireDate = DateTime.Now;
        bool isActive = true; // Employee default aktiv olacaq

        // Position seçimi - yalnız 1 position qəbul edilir
        Console.Write("Employee Position ID daxil edin (yalnız 1): ");
        var positionInput = Console.ReadLine()!;
        if (!int.TryParse(positionInput.Trim(), out int positionId) || positionId <= 0)
        {
            Console.WriteLine("Position ID düzgün deyil");
            Console.ReadLine();
            return;
        }

        // DTO yarat
        var dto = new CreateEmployeeRequest
        {
            FullName = fullName,
            Email = email,
            Phone = phone,
            FinCode = finCode,
            BirthDate = birthDate,
            HireDate = hireDate,
            IsActive = isActive,
            PositionIds = new List<int> { positionId } // Yalnız 1 position
        };

        // Validator çağır
        var validator = new CreateEmployeeValidation(_context);
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

        // Employee yarat və yalnız 1 position aktiv olsun
        var employee = new Employee
        {
            FullName = dto.FullName,
            Email = dto.Email,
            Phone = dto.Phone,
            FinCode = dto.FinCode,
            BirthDate = dto.BirthDate,
            HireDate = dto.HireDate,
            IsActive = dto.IsActive
        };
        _context.Employees.Add(employee);
        _context.SaveChanges(); // Employee ID artıq mövcuddur

        // 2. EmployeePosition əlavə et
        var employeePosition = new EmployeePosition
        {
            EmployeeId = employee.Id,
            PositionId = positionId,
            IsActive = true
        };
    

        /*_context.Employees.Add(employeePosition);
        _context.SaveChanges();*/

        Console.WriteLine("\nEmployee uğurla əlavə olundu!");
        Console.WriteLine("Davam etmək üçün Enter basın...");
        Console.ReadLine();
    }

}
