using AlisRestaurant.Data.Context;
using AlisRestaurant.DTOs.RestaurantDto;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AlisRestaurant.Validations.RestaurantValidations;

public class CreateRestaurantValidation:AbstractValidator<CreateRestaurantRequest>
{
    private readonly AppDbContext _context;
    public CreateRestaurantValidation(AppDbContext dbContext)
    {
        _context = dbContext;
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Restoran adı boş ola bilməz")
            .MaximumLength(200).WithMessage("Restoran adı maksimum 200 simvol ola bilər")
            .MustAsync(BeUniqueName).WithMessage("Bu restoran adı artıq mövcuddur");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Ünvan boş ola bilməz")
            .MaximumLength(300).WithMessage("Ünvan maksimum 300 simvol ola bilər");
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Telefon nömrəsi boş ola bilməz")
            .MaximumLength(20).WithMessage("Telefon nömrəsi maksimum 20 simvol ola bilər");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş ola bilməz")
            .MaximumLength(100).WithMessage("Email maksimum 100 simvol ola bilər")
            .EmailAddress().WithMessage("Email düzgün formatda deyil");
        
        RuleFor(x => x.CompanyId)
            .NotEmpty().WithMessage("Şirkət ID-si boş ola bilməz")
            .GreaterThan(0).WithMessage("Şirkət ID-si 0-dan böyük olmalıdır")
            .ChildRules(companyId =>
            {
                companyId.RuleFor(x => x)
                    .MustAsync(async (companyIdValue, cancellationToken) =>
                    {
                        return await _context.Companies
                            .AnyAsync(c => c.Id == companyIdValue, cancellationToken);
                    })
                    .WithMessage("Belə bir şirkət mövcud deyil");
            });
    }

    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return !await _context.Restaurants
            .AnyAsync(x => x.Name.ToLower() == name.ToLower(), cancellationToken);
    }
}
