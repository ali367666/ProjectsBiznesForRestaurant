using AlisRestaurant.Data.Context;
using AlisRestaurant.DTOs.HRDto.Employee;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace AlisRestaurant.Validations.EmployeeValidation
{
    public class UpdateEmployeeValidation : AbstractValidator<UpdateEmployeeRequest>
    {
        private readonly AppDbContext _context;

        public UpdateEmployeeValidation(AppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Employee ID mütləq daxil edilməlidir və pozitiv tam ədəd olmalıdır")
                .MustAsync(ExistInDatabase)
                .WithMessage("Bu ID ilə employee mövcud deyil");

            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Full Name boş ola bilməz")
                .MaximumLength(100)
                .WithMessage("Full Name maksimum 100 simvol ola bilər")
                .Must(name => !string.IsNullOrWhiteSpace(name))
                .WithMessage("Full Name boş ola bilməz");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("Phone boş ola bilməz")
                .Must(BeValidPhoneFormat)
                .WithMessage("Phone düzgün formatda olmalıdır (məsələn: +994501234567)")
                .MustAsync((request, phone, cancellationToken) => BeUniquePhone(request.Id, phone, cancellationToken))
                .WithMessage("Bu telefon nömrəsi artıq istifadə olunur");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email boş ola bilməz")
                .EmailAddress()
                .WithMessage("Email düzgün email formatında olmalıdır")
                .MustAsync((request, email, cancellationToken) => BeUniqueEmail(request.Id, email, cancellationToken))
                .WithMessage("Bu email artıq istifadə olunur");

            RuleFor(x => x.HireDate)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Hire Date gələcək tarix ola bilməz");

            RuleFor(x => x.FinCode)
                .NotEmpty()
                .WithMessage("FinCode boş ola bilməz")
                .Length(7)
                .WithMessage("FinCode dəqiq 7 simvol olmalıdır")
                .MustAsync((request, finCode, cancellationToken) => BeUniqueFinCode(request.Id, finCode, cancellationToken))
                .WithMessage("Bu FinCode artıq istifadə olunur");

            RuleFor(x => x.BirthDate)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("Birth Date gələcək tarix ola bilməz")
                .Must(BeAtLeast18YearsOld)
                .WithMessage("Employee ən azı 18 yaşında olmalıdır");

            RuleFor(x => x.PositionIds)
                .NotEmpty()
                .WithMessage("Ən azı 1 position seçilməlidir")
                .MustAsync(AllPositionsExist)
                .WithMessage("Bütün Position ID-ləri mövcud olmalıdır");
        }

        private async Task<bool> ExistInDatabase(int employeeId, CancellationToken cancellationToken)
        {
            return await _context.Employees
                .AnyAsync(e => e.Id == employeeId, cancellationToken);
        }

        private bool BeValidPhoneFormat(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            // Format: +994501234567 (Azərbaycan telefon nömrəsi formatı)
            var phoneRegex = new Regex(@"^\+994\d{9}$");
            return phoneRegex.IsMatch(phone);
        }

        private bool BeAtLeast18YearsOld(DateOnly birthDate)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var age = today.Year - birthDate.Year;

            // Əgər bu ilki doğum günü hələ keçməyibsə, yaşı 1 azalt
            if (birthDate > today.AddYears(-age))
                age--;

            return age >= 18;
        }

        private async Task<bool> BeUniqueEmail(int employeeId, string email, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(email))
                return true;

            // Cari employee-ni istisna edirik
            return !await _context.Employees
                .AnyAsync(e => e.Id != employeeId && e.Email.ToLower() == email.ToLower(), cancellationToken);
        }

        private async Task<bool> BeUniquePhone(int employeeId, string phone, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return true;

            // Cari employee-ni istisna edirik
            return !await _context.Employees
                .AnyAsync(e => e.Id != employeeId && e.Phone == phone, cancellationToken);
        }

        private async Task<bool> BeUniqueFinCode(int employeeId, string finCode, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(finCode))
                return true;

            // Cari employee-ni istisna edirik
            return !await _context.Employees
                .AnyAsync(e => e.Id != employeeId && e.FinCode.ToLower() == finCode.ToLower(), cancellationToken);
        }

        private async Task<bool> AllPositionsExist(List<int> positionIds, CancellationToken cancellationToken)
        {
            if (positionIds == null || !positionIds.Any())
                return false;

            var existingPositions = await _context.Positions
                .Where(p => positionIds.Contains(p.Id))
                .Select(p => p.Id)
                .ToListAsync(cancellationToken);

            return existingPositions.Count == positionIds.Count;
        }
    }
}
