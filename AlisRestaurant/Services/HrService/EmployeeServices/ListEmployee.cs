using AlisRestaurant.Data.Context;

namespace AlisRestaurant.Services.HrService.EmployeeServices;

public class ListEmployee
{
    private readonly AppDbContext _context;

    

    public ListEmployee()
    {
        _context = new AppDbContext();
    }

    public void Execute()
    {
        Console.Clear();
        Console.WriteLine("=== Bütün Employees ===\n");

        var employees = _context.Employees
            .OrderBy(e => e.Id)
            .ToList();

        if (!employees.Any())
        {
            Console.WriteLine("Heç bir employee tapılmadı.");
        }
        else
        {
            Console.WriteLine("ID\tFullName\tEmail\tPhone\tPosition(s)");
            Console.WriteLine("------------------------------------------------------");

            foreach (var emp in employees)
            {
                var positions = emp.EmployeePositions
                    .Select(ep => _context.Positions.FirstOrDefault(p => p.Id == ep.PositionId)?.Name)
                    .Where(n => n != null);

                Console.WriteLine($"{emp.Id}\t{emp.FullName}\t{emp.Email}\t{emp.Phone}\t{string.Join(", ", positions)}");
            }
        }

        Console.WriteLine("\nDavam etmək üçün Enter basın...");
        Console.ReadLine();
    }
}
