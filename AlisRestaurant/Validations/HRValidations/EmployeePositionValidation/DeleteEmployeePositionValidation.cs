using AlisRestaurant.Data.Context;
using AlisRestaurant.DTOs.HRDto.EmployeePosition;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AlisRestaurant.Validations
{
    public class DeleteEmployeePositionValidation : AbstractValidator<DeleteEmployeePositionRequest>
    {
        private readonly AppDbContext _context;

        public DeleteEmployeePositionValidation(AppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("EmployeePosition ID mütləq daxil edilməlidir")
                .MustAsync(EmployeePositionExists).WithMessage("Belə EmployeePosition mövcud deyil")
                .MustAsync(NotUsedElsewhere).WithMessage("Bu EmployeePosition digər cədvəllərdə istifadə olunub, silmək mümkün deyil");
        }

        private async Task<bool> EmployeePositionExists(int id, CancellationToken cancellationToken)
        {
            return await _context.EmployeePositions.AnyAsync(ep => ep.Id == id, cancellationToken);
        }

        private async Task<bool> NotUsedElsewhere(int id, CancellationToken cancellationToken)
        {
            // Burada yoxlayırsan ki, bu təyinat digər cədvəllərdə istifadə olunmayıb
            // Məsələn, əgər əlavə bağımlı cədvəllərin varsa, onları burda yoxlaya bilərsən
            // Əgər yoxdursa, həmişə true qaytar
            return true;
        }
    }
}
