using AlisRestaurant.Data.Context;
using AlisRestaurant.DTOs.CompanyDto;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AlisRestaurant.Validations.CompanyValidations;

public class CreateCompanyValidation:AbstractValidator<CreateCompanyRequest>
{
    private readonly AppDbContext _context;
    public CreateCompanyValidation(AppDbContext context)
    {
        _context = context;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Şirkət adı boş ola bilməz")
            .MustAsync(BeUniqueName).WithMessage("Bu şirkət adı artıq mövcuddur")
            .MaximumLength(100).WithMessage("Şirkət adı maksimum 100 simvol ola bilər");

            
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
    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return !await _context.Companies
            .AnyAsync(x => x.Name.ToLower() == name.ToLower(), cancellationToken);
    }



}
