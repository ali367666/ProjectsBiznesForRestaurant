using AlisRestaurant.Data.Context;
using AlisRestaurant.DTOs.CompanyDto;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AlisRestaurant.Validations.CompanyValidations;

public class DeleteCompanyValidation:AbstractValidator<DeleteCompanyRequest>
{
    private readonly AppDbContext _context;
    public DeleteCompanyValidation(AppDbContext context)
    {
        _context = context;
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Şirkət ID-si boş ola bilməz")
            .GreaterThan(0).WithMessage("Şirkət ID-si 0-dan böyük olmalıdır");
    }
    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(name))
            return true;

        return !await _context.Companies
            .AnyAsync(d => d.Name.Trim().ToLower() == name.Trim().ToLower(), cancellationToken);
    }

}
