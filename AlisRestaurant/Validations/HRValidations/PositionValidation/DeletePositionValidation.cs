using AlisRestaurant.Data.Context;
using AlisRestaurant.DTOs.HRDto.Position;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AlisRestaurant.Validations.HRValidations.PositionValidation
{
    public class DeletePositionValidation : AbstractValidator<DeletePositionRequest>
    {
        private readonly AppDbContext _context;

        public DeletePositionValidation()
        {
        }

        public DeletePositionValidation(AppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Position ID mütləq daxil edilməlidir və pozitiv tam ədəd olmalıdır")
                .MustAsync(ExistInDatabase)
                .WithMessage("Bu ID ilə position mövcud deyil")
                .MustAsync(NotBeUsedInOtherTables)
                .WithMessage("Bu position digər cədvəllərdə (EmployeePosition) istifadə olunduğu üçün silinə bilməz");
        }

        private async Task<bool> ExistInDatabase(int id, CancellationToken cancellationToken)
        {
            return await _context.Positions
                .AnyAsync(p => p.Id == id, cancellationToken);
        }

        private async Task<bool> NotBeUsedInOtherTables(int id, CancellationToken cancellationToken)
        {
            // Position-un EmployeePosition-larda istifadəsi yoxlanılır
            var hasEmployeePositions = await _context.EmployeePositions
                .AnyAsync(ep => ep.PositionId == id, cancellationToken);

            if (hasEmployeePositions)
                return false;

            return true;
        }
    }
}

