using AlisRestaurant.Data.Context;
using AlisRestaurant.DTOs.HRDto.Position;
using AlisRestaurant.Validations.HRValidations.PositionValidation;

namespace AlisRestaurant.Services.HrService.PositionServices;

public class UpdatePosition
{
    private readonly AppDbContext _dbContext;
    public UpdatePosition()
    {
        _dbContext = new AppDbContext();
    }
    public void Execute()
    {
        Console.Clear();
        Console.WriteLine("=== Position Yenilə ===");
        Console.Write("Yeniləmək istədiyiniz Position ID daxil edin: ");
        var idInput = Console.ReadLine();
        if (!int.TryParse(idInput, out int positionId))
        {
            Console.WriteLine("Position ID düzgün deyil");
            Console.ReadLine();
            return;
        }
        Console.Write("Yeni Position adı daxil edin: ");
        var name = Console.ReadLine();
        Console.Write("Yeni Department ID daxil edin: ");
        var departmentIdInput = Console.ReadLine();
        if (!int.TryParse(departmentIdInput, out int departmentId))
        {
            Console.WriteLine("Department ID düzgün deyil");
            Console.ReadLine();
            return;
        }
        var dto = new UpdatePositionRequest
        {
            Id = positionId,
            Name = name!,
            DepartmentId = departmentId
        };
        var validator = new UpdatePositionValidation(_dbContext);
        FluentValidation.Results.ValidationResult result = validator.Validate(dto);
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
        position.Name = dto.Name;
        position.DepartmentId = dto.DepartmentId;
        _dbContext.SaveChanges();
        Console.WriteLine("\nPosition uğurla yeniləndi!");
        Console.WriteLine("Davam etmək üçün Enter basın...");
        Console.ReadLine();
    }
}
