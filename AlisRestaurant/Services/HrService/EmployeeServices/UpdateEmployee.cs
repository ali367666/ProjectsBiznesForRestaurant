using AlisRestaurant.Data.Context;
using AlisRestaurant.Data.Entities.HR;
using AlisRestaurant.DTOs.HRDto.Employee;
using AlisRestaurant.Validations.EmployeeValidation;

namespace AlisRestaurant.Services.HrService.EmployeeServices;

public class UpdateEmployee
{
    private readonly AppDbContext _context;

    public UpdateEmployee()
    {
        _context = new AppDbContext();
    }

    public void Execute()
    {
        Console.Clear();
        Console.WriteLine("=== Employee Yenilə ===");

        Console.Write("Yenilənəcək Employee ID daxil edin: ");
        var idInput = Console.ReadLine();
        if (!int.TryParse(idInput, out int employeeId))
        {
            Console.WriteLine("ID düzgün deyil");
            Console.ReadLine();
            return;
        }

        var employee = _context.Employees
            .FirstOrDefault(e => e.Id == employeeId);

        if (employee == null)
        {
            Console.WriteLine("Belə ID-li employee tapılmadı");
            Console.ReadLine();
            return;
        }

        Console.Write($"Yeni FullName daxil edin (hazırkı: {employee.FullName}): ");
        var fullName = Console.ReadLine();
        fullName = string.IsNullOrWhiteSpace(fullName) ? employee.FullName : fullName;

        Console.Write($"Yeni Email daxil edin (hazırkı: {employee.Email}): ");
        var email = Console.ReadLine();
        email = string.IsNullOrWhiteSpace(email) ? employee.Email : email;

        Console.Write($"Yeni Phone daxil edin (hazırkı: {employee.Phone}): ");
        var phone = Console.ReadLine();
        phone = string.IsNullOrWhiteSpace(phone) ? employee.Phone : phone;

        Console.Write($"Yeni Position ID daxil edin (hazırkı: {string.Join(",", employee.EmployeePositions.Select(ep => ep.PositionId))}): ");
        var posInput = Console.ReadLine();
        var positionIds = string.IsNullOrWhiteSpace(posInput)
            ? employee.EmployeePositions.Select(ep => ep.PositionId).ToList()
            : posInput.Split(',', StringSplitOptions.RemoveEmptyEntries)
                      .Select(s => int.TryParse(s.Trim(), out int id) ? id : -1)
                      .Where(id => id > 0)
                      .ToList();

        var dto = new UpdateEmployeeRequest
        {
            Id = employeeId,
            FullName = fullName,
            Email = email,
            Phone = phone,
            PositionIds = positionIds
        };

        var validator = new UpdateEmployeeValidation(_context);
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

        employee.FullName = dto.FullName;
        employee.Email = dto.Email;
        employee.Phone = dto.Phone;

        employee.EmployeePositions.Clear();
        employee.EmployeePositions = dto.PositionIds.Select(pid => new EmployeePosition
        {
            PositionId = pid
        }).ToList();

        _context.SaveChanges();

        Console.WriteLine("\nEmployee uğurla yeniləndi!");
        Console.WriteLine("Davam etmək üçün Enter basın...");
        Console.ReadLine();
    }

}
