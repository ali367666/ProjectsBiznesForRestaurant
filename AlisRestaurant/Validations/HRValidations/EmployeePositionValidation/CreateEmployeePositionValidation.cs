using AlisRestaurant.Data.Context;
using AlisRestaurant.DTOs.HRDto.EmployeePosition;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AlisRestaurant.Validations
{
    public class CreateEmployeePositionValidation : AbstractValidator<CreateEmployeePositionRequest>
    {
        private readonly AppDbContext _context;

        public CreateEmployeePositionValidation(AppDbContext context)
        {
            _context = context;

            RuleFor(x => x.EmployeeId)
                .GreaterThan(0).WithMessage("Employee ID mütləq daxil edilməlidir")
                .MustAsync(EmployeeExists).WithMessage("Belə Employee mövcud deyil");

            RuleFor(x => x.PositionId)
                .GreaterThan(0).WithMessage("Position ID mütləq daxil edilməlidir")
                .MustAsync(PositionExists).WithMessage("Belə Position mövcud deyil");

            RuleFor(x => x.AssignedDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Assigned Date gələcək tarix ola bilməz");

            RuleFor(x => x)
                .MustAsync(NotDuplicateActiveAssignment)
                .WithMessage("Bu employee artıq bu position-da aktivdir! Təkrar təyin etmək olmaz.");
        }

        private async Task<bool> EmployeeExists(int employeeId, CancellationToken cancellationToken)
        {
            return await _context.Employees.AnyAsync(e => e.Id == employeeId, cancellationToken);
        }

        private async Task<bool> PositionExists(int positionId, CancellationToken cancellationToken)
        {
            return await _context.Positions.AnyAsync(p => p.Id == positionId, cancellationToken);
        }

        private async Task<bool> NotDuplicateActiveAssignment(CreateEmployeePositionRequest request, CancellationToken cancellationToken)
        {
            return !await _context.EmployeePositions
                .AnyAsync(ep =>
                    ep.EmployeeId == request.EmployeeId &&
                    ep.PositionId == request.PositionId &&
                    ep.IsActive, cancellationToken);
        }
    }
}
