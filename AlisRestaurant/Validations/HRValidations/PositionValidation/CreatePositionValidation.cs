using AlisRestaurant.Data.Context;
using AlisRestaurant.DTOs.HRDto.Position;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AlisRestaurant.Validations.HRValidations.PositionValidation
{
    public class CreatePositionValidation : AbstractValidator<CreatePositionRequest>
    {
        private readonly AppDbContext _context;

        public CreatePositionValidation(AppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Position adı boş ola bilməz")
                .MaximumLength(50)
                .WithMessage("Position adı maksimum 50 simvol ola bilər")
                .Must(name => !string.IsNullOrWhiteSpace(name))
                .WithMessage("Position adı boş ola bilməz");

            RuleFor(x => x)
                .MustAsync((request, cancellationToken) => BeUniqueInDepartment(request.DepartmentId, request.Name, cancellationToken))
                .WithMessage("Bu department-də bu adda position artıq mövcuddur")
                .When(x => !string.IsNullOrWhiteSpace(x.Name) && x.DepartmentId > 0);

            RuleFor(x => x.DepartmentId)
                .GreaterThan(0)
                .WithMessage("Department ID mütləq daxil edilməlidir və pozitiv tam ədəd olmalıdır")
                .MustAsync(ExistInDatabase)
                .WithMessage("Bu ID ilə department mövcud deyil");
        }

        private async Task<bool> ExistInDatabase(int departmentId, CancellationToken cancellationToken)
        {
            return await _context.Departments
                .AnyAsync(d => d.Id == departmentId, cancellationToken);
        }

        private async Task<bool> BeUniqueInDepartment(int departmentId, string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(name))
                return true;

            return !await _context.Positions
                .AnyAsync(p => p.DepartmentId == departmentId &&
                              p.Name.Trim().ToLower() == name.Trim().ToLower(),
                          cancellationToken);
        }
    }
}

