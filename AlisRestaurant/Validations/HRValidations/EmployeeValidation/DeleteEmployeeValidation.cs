using AlisRestaurant.Data.Context;
using AlisRestaurant.DTOs.HRDto.Employee;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AlisRestaurant.Validations.EmployeeValidation
{
    public class DeleteEmployeeValidation : AbstractValidator<DeleteEmployeeRequest>
    {
        private readonly AppDbContext _context;

        public DeleteEmployeeValidation()
        {
        }

        public DeleteEmployeeValidation(AppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Employee ID mütləq daxil edilməlidir və pozitiv tam ədəd olmalıdır")
                .MustAsync(ExistInDatabase)
                .WithMessage("Bu ID ilə employee mövcud deyil")
                .MustAsync(NotBeUsedInOtherTables)
                .WithMessage("Bu employee digər cədvəllərdə (EmployeePosition) istifadə olunduğu üçün silinə bilməz")
                .WithMessage("18 yaşdan aşağı olan employee silinə bilməz");
        }

        private async Task<bool> ExistInDatabase(int id, CancellationToken cancellationToken)
        {
            return await _context.Employees
                .AnyAsync(e => e.Id == id, cancellationToken);
        }

        private async Task<bool> NotBeUsedInOtherTables(int id, CancellationToken cancellationToken)
        {
            // Employee-nin EmployeePosition-larda istifadəsi yoxlanılır
            var hasEmployeePositions = await _context.EmployeePositions
                .AnyAsync(ep => ep.EmployeeId == id, cancellationToken);

            if (hasEmployeePositions)
                return false;

            return true;
        }

        
    }
}
