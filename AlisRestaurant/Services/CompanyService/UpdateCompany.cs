using AlisRestaurant.Data.Context;
using AlisRestaurant.Validations.CompanyValidations;

namespace AlisRestaurant.Services.CompanyService;

public class UpdateCompany
{
    private readonly AppDbContext _context;
    public UpdateCompany()
    {
        _context = new AppDbContext();
    }

    public void Execute()
    {
        Console.Clear();
        Console.WriteLine("=== Şirkət Yeniləmə ===");
        Console.Write("Yeniləmək istədiyiniz şirkətin ID-sini daxil edin: ");
        if (!int.TryParse(Console.ReadLine(), out int companyId))
        {
            Console.WriteLine("Düzgün ID daxil edin.");
            Console.ReadLine();
            return;
        }
        var company = _context.Companies.Find(companyId);
        if (company == null)
        {
            Console.WriteLine("Şirkət tapılmadı.");
            Console.ReadLine();
            return;
        }
        Console.Write("Yeni şirkət adı (mövcud: {0}): ", company.Name);
        var name = Console.ReadLine();
        Console.Write("Yeni ünvan (mövcud: {0}): ", company.Address);
        var address = Console.ReadLine();
        Console.Write("Yeni telefon nömrəsi (mövcud: {0}): ", company.PhoneNumber);
        var phoneNumber = Console.ReadLine();
        
        var dto=new DTOs.CompanyDto.UpdateCompanyRequest{
            Id = companyId,
            Name = string.IsNullOrWhiteSpace(name) ? company.Name : name,
            Address = string.IsNullOrWhiteSpace(address) ? company.Address : address,
            PhoneNumber = string.IsNullOrWhiteSpace(phoneNumber) ? company.PhoneNumber : phoneNumber,
            Email = company.Email // Email dəyişdirilmir
        };
        
        var validator = new UpdateCompanyValidation(_context);
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
        company.Name = dto.Name;
        company.Address = dto.Address;
        company.PhoneNumber = dto.PhoneNumber;
        _context.SaveChanges();
        Console.WriteLine("\nŞirkət uğurla yeniləndi!");
        Console.WriteLine("Davam etmək üçün Enter basın...");
        Console.ReadLine();

    }
}
