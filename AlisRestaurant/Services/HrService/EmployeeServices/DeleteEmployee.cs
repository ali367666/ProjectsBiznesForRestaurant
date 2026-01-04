using AlisRestaurant.Data.Context;
using AlisRestaurant.DTOs.HRDto.Employee;
using AlisRestaurant.Validations.EmployeeValidation;

namespace AlisRestaurant.Services.HrService.EmployeeServices;

public class DeleteEmployee
{
    private readonly AppDbContext _context;

   

    public DeleteEmployee()
    {
        _context = new AppDbContext();
    }

    public void Execute()
    {
        Console.Clear();
        Console.WriteLine("=== Employee Sil ===");

        Console.Write("Silmək istədiyiniz Employee ID daxil edin: ");
        var input = Console.ReadLine();
        if (!int.TryParse(input, out int employeeId))
        {
            Console.WriteLine("ID düzgün deyil");
            Console.ReadLine();
            return;
        }

        var dto = new DeleteEmployeeRequest { Id = employeeId };
        var validator = new DeleteEmployeeValidation();
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

        var employee = _context.Employees.Find(dto.Id);
        if (employee == null)
        {
            Console.WriteLine("Belə ID-li employee tapılmadı");
            Console.ReadLine();
            return;
        }

        _context.Employees.Remove(employee);
        _context.SaveChanges();

        Console.WriteLine("\nEmployee uğurla silindi!");
        Console.WriteLine("Davam etmək üçün Enter basın...");
        Console.ReadLine();
    }
}
