using AlisRestaurant.Data.Context;
using AlisRestaurant.DTOs.HRDto.EmployeePosition;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AlisRestaurant.Validations
{
    public class UpdateEmployeePositionValidation : AbstractValidator<UpdateEmployeePositionRequest>
    {
        private readonly AppDbContext _context;

        public UpdateEmployeePositionValidation(AppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("EmployeePosition ID mütləq daxil edilməlidir")
                .MustAsync(EmployeePositionExists).WithMessage("Belə EmployeePosition mövcud deyil");

            RuleFor(x => x.EndDate)
                .LessThanOrEqualTo(DateTime.Now)
                .When(x => x.EndDate.HasValue)
                .WithMessage("EndDate gələcək tarix ola bilməz");

            RuleFor(x => x)
                .MustAsync(EndDateAfterAssignedDate)
                .WithMessage("EndDate AssignedDate-dən kiçik ola bilməz");
        }

        private async Task<bool> EmployeePositionExists(int id, CancellationToken cancellationToken)
        {
            return await _context.EmployeePositions.AnyAsync(ep => ep.Id == id, cancellationToken);
        }

        private async Task<bool> EndDateAfterAssignedDate(UpdateEmployeePositionRequest request, CancellationToken cancellationToken)
        {
            if (!request.EndDate.HasValue)
                return true; // EndDate verilməyibsə, problem yoxdur

            var ep = await _context.EmployeePositions
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (ep == null)
                return false;

            return request.EndDate.Value >= ep.AssignedDate;
        }
    }
}

