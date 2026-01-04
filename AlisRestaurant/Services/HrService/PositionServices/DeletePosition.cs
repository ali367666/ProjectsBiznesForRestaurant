using AlisRestaurant.Data.Context;
using AlisRestaurant.Data.Entities.HR;
using AlisRestaurant.DTOs.HRDto.Department;
using AlisRestaurant.DTOs.HRDto.Position;
using AlisRestaurant.Validations.HRValidations.DepartmentValidation;
using AlisRestaurant.Validations.HRValidations.PositionValidation;
using Microsoft.EntityFrameworkCore;
public class DeletePosition
{
    private readonly AppDbContext _dbContext;
    public DeletePosition()
    {
        _dbContext = new AppDbContext();
    }
    public void Execute()
    {
        Console.Clear();
        Console.WriteLine("=== Position Sil ===");

        Console.Write("Silmək istədiyiniz Position ID daxil edin: ");
        var idInput = Console.ReadLine();

        if (!int.TryParse(idInput, out int positionId))
        {
            Console.WriteLine("Position ID düzgün deyil");
            Console.ReadLine();
            return;
        }

        var dto = new DeletePositionRequest
        {
            Id = positionId
        };

        var validator = new DeletePositionValidation();
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

        var position = _dbContext.Positions.Find(dto.Id);
        if (position == null)
        {
            Console.WriteLine("Belə ID-li position tapılmadı");
            Console.ReadLine();
            return;
        }

        _dbContext.Positions.Remove(position);
        _dbContext.SaveChanges();

        Console.WriteLine("\nPosition uğurla silindi!");
        Console.WriteLine("Davam etmək üçün Enter basın...");
        Console.ReadLine();
    }

   
}
