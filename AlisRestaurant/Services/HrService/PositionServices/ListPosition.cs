using AlisRestaurant.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AlisRestaurant.Services.HrService.PositionServices;

public class ListPosition
{
    private readonly AppDbContext _dbContext;
    public ListPosition()
    {
        _dbContext = new AppDbContext();
    }
    public void Execute()
    {
        Console.Clear();
        Console.WriteLine("=== Bütün Position-lar ===\n");

        var positions = _dbContext.Positions
            .OrderBy(p => p.Id) // ID-yə görə sırala
            .ToList();

        if (!positions.Any())
        {
            Console.WriteLine("Heç bir position tapılmadı.");
        }
        else
        {
            Console.WriteLine("ID\tName\t\tDepartmentID");
            Console.WriteLine("--------------------------------------");

            foreach (var position in positions)
            {
                Console.WriteLine($"{position.Id}\t{position.Name}\t\t{position.DepartmentId}");
            }
        }

        Console.WriteLine("\nDavam etmək üçün Enter basın...");
        Console.ReadLine();
    }


}
