using AlisRestaurant.Data.Context;
using AlisRestaurant.DTOs.HRDto.Department;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AlisRestaurant.Validations.HRValidations.DepartmentValidation
{
    public class CreateDepartmentValidation : AbstractValidator<CreateDepartmentRequest>
    {
        private readonly AppDbContext _context;

        public CreateDepartmentValidation(AppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Department adı boş ola bilməz")
                .MaximumLength(50)
                .WithMessage("Department adı maksimum 50 simvol ola bilər")
                .Must(name => !string.IsNullOrWhiteSpace(name))
                .WithMessage("Department adı boş ola bilməz")
                .MustAsync(BeUniqueName)
                .WithMessage("Bu adda department artıq mövcuddur");
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(name))
                return true;

            return !await _context.Departments
                .AnyAsync(d => d.Name.Trim().ToLower() == name.Trim().ToLower(), cancellationToken);
        }
    }
}
