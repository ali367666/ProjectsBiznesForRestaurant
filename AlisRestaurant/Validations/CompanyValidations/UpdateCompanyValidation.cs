using AlisRestaurant.Data.Context;
using AlisRestaurant.DTOs.CompanyDto;
using AlisRestaurant.DTOs.HRDto.Department;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AlisRestaurant.Validations.CompanyValidations;

    
public class UpdateCompanyValidation : AbstractValidator<UpdateCompanyRequest>
{
 
    private readonly AppDbContext _context;

    public UpdateCompanyValidation(AppDbContext context)
    {
        _context = context;
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Şirkət ID mütləq daxil edilməlidir və pozitiv tam ədəd olmalıdır")
            .MustAsync(ExistInDatabase)
            .WithMessage("Bu ID ilə şirkət mövcud deyil");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Şirkət adı boş ola bilməz")
            .MaximumLength(100).WithMessage("Şirkət adı maksimum 100 simvol ola bilər")
            .WithMessage("Bu adda şirkət artıq mövcuddur");
        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Ünvan boş ola bilməz")
            .MaximumLength(200).WithMessage("Ünvan maksimum 200 simvol ola bilər");
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Telefon nömrəsi boş ola bilməz")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Telefon nömrəsi düzgün formatda deyil");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş ola bilməz")
            .EmailAddress().WithMessage("Email düzgün formatda deyil");



    }

    private async Task<bool> ExistInDatabase(int id, CancellationToken cancellationToken)
    {
        return await _context.Companies
            .AnyAsync(d => d.Id == id, cancellationToken);
    }

    private async Task<bool> BeUniqueName(UpdateCompanyRequest request, string name, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(name))
            return true;

        return !await _context.Departments
            .AnyAsync(d => d.Id != request.Id &&
                          d.Name.Trim().ToLower() == name.Trim().ToLower(),
                          cancellationToken);
    }

}
