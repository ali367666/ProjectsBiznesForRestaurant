using AlisRestaurant.Data.Context;
using AlisRestaurant.DTOs.HRDto.Department;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AlisRestaurant.Validations.HRValidations.DepartmentValidation
{
    public class DeleteDepartmentValidation : AbstractValidator<DeleteDepartmentRequest>
    {
        private readonly AppDbContext _context;

        public DeleteDepartmentValidation(AppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Department ID mütləq daxil edilməlidir və pozitiv tam ədəd olmalıdır")
                .MustAsync(ExistInDatabase)
                .WithMessage("Bu ID ilə department mövcud deyil")
                .MustAsync(NotBeUsedInOtherTables)
                .WithMessage("Bu department digər cədvəllərdə (Position və ya Employee) istifadə olunduğu üçün silinə bilməz");
        }

        private async Task<bool> ExistInDatabase(int id, CancellationToken cancellationToken)
        {
            return await _context.Departments
                .AnyAsync(d => d.Id == id, cancellationToken);
        }

        private async Task<bool> NotBeUsedInOtherTables(int id, CancellationToken cancellationToken)
        {
            // Department-in Position-larda istifadəsi yoxlanılır
            var hasPositions = await _context.Positions
                .AnyAsync(p => p.DepartmentId == id, cancellationToken);

            if (hasPositions)
                return false;

            return true;
        }
    }
}

