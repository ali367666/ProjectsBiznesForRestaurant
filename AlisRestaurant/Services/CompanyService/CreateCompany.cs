using AlisRestaurant.Data.Context;
using AlisRestaurant.Data.Entities;
using AlisRestaurant.DTOs.CompanyDto;
using AlisRestaurant.Validations.CompanyValidations;

namespace AlisRestaurant.Services.CompanyService;

public class CreateCompany
{
    private readonly AppDbContext _context;
    public CreateCompany()
    {
        _context = new AppDbContext();
    }
    public void Execute()
    {
        Console.Clear();
        Console.WriteLine("Yeni sirket elave edin");
        Console.Write("Sirket adi: ");
        var name = Console.ReadLine()!;
        Console.Write("Sirket unvani: ");
        var address = Console.ReadLine()!;
        Console.Write("Sirket telefon nomresi: ");
        var phoneNumber = Console.ReadLine()!;
        Console.Write("Sirket emaili: ");
        var email = Console.ReadLine()!;
        
        var dto=new CreateCompanyRequest{
            Name = name,
            Address = address,
            PhoneNumber = phoneNumber,
            Email = email
        };
        var validator = new CreateCompanyValidation(_context);
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
        var company = new Company{
            Name = dto.Name,
            Address = dto.Address,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email
        };
        _context.Companies.Add(company);
        _context.SaveChanges();
        Console.WriteLine("\nŞirkət uğurla əlavə olundu!");
        Console.WriteLine("Davam etmək üçün Enter basın...");
        Console.ReadLine();
    }
}
