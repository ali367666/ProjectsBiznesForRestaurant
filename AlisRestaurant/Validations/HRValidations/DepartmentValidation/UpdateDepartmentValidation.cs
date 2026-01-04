using AlisRestaurant.Data.Context;
using AlisRestaurant.DTOs.HRDto.Department;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AlisRestaurant.Validations.HRValidations.DepartmentValidation
{
    public class UpdateDepartmentValidation : AbstractValidator<UpdateDepartmentRequest>
    {
        private readonly AppDbContext _context;

        public UpdateDepartmentValidation(AppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Department ID mütləq daxil edilməlidir və pozitiv tam ədəd olmalıdır")
                .MustAsync(ExistInDatabase)
                .WithMessage("Department ID mövcud deyil");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Department adı boş ola bilməz")
                .MaximumLength(50)
                .WithMessage("Department adı maksimum 50 simvol ola bilər")
                .Must(name => !string.IsNullOrWhiteSpace(name))
                .WithMessage("Department adı boş ola bilməz")
                .MustAsync((request, name, cancellationToken) => BeUniqueName(request, name, cancellationToken))
                .WithMessage("Bu adda department artıq mövcuddur");
        }

        private async Task<bool> ExistInDatabase(int id, CancellationToken cancellationToken)
        {
            return await _context.Departments
                .AnyAsync(d => d.Id == id, cancellationToken);
        }

        private async Task<bool> BeUniqueName(UpdateDepartmentRequest request, string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(name))
                return true;

            return !await _context.Departments
                .AnyAsync(d => d.Id != request.Id &&
                              d.Name.Trim().ToLower() == name.Trim().ToLower(),
                              cancellationToken);
        }
    }
}
