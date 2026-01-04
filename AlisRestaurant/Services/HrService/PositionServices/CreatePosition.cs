using AlisRestaurant.Data.Context;
using AlisRestaurant.Data.Entities.HR;
using AlisRestaurant.DTOs.HRDto.Department;
using AlisRestaurant.DTOs.HRDto.Position;
using AlisRestaurant.Validations.HRValidations.DepartmentValidation;
using AlisRestaurant.Validations.HRValidations.PositionValidation;

namespace AlisRestaurant.Services.HrService.PositionServices;

public class CreatePosition
{
    private readonly AppDbContext _dbContext;

    public CreatePosition()
    {
        _dbContext = new AppDbContext();
    }

    public void Execute()
    {
        Console.Clear();
        Console.WriteLine("=== Yeni Position Yarat ===");

        // 1️⃣ User input
        Console.Write("Position adi daxil edin: ");
        var name = Console.ReadLine();

        Console.Write("Department id daxil edin: ");
        var departmentIdInput = Console.ReadLine();

        if (!int.TryParse(departmentIdInput, out int departmentId))
        {
            Console.WriteLine("Department ID düzgün deyil");
            Console.ReadLine();
            return;
        }

        var dto = new CreatePositionRequest
        {
            Name = name!,
            DepartmentId = departmentId
        };

        // 3️⃣ Validator çağır
        var validator = new CreatePositionValidation(_dbContext);
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

        // 4️⃣ DB əməliyyatı
        var position = new Position
        {
            Name = dto.Name,
            DepartmentId = dto.DepartmentId
        };
        _dbContext.Positions.Add(position);
        _dbContext.SaveChanges();

        Console.WriteLine("\nPosition uğurla əlavə olundu!");
        Console.WriteLine("Davam etmək üçün Enter basın...");
        Console.ReadLine();
    }
}
