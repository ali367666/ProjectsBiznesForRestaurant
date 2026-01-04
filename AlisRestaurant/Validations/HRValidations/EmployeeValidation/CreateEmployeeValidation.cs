using AlisRestaurant.Data.Context;
using AlisRestaurant.DTOs.HRDto.Employee;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace AlisRestaurant.Validations.EmployeeValidation
{
    public class CreateEmployeeValidation : AbstractValidator<CreateEmployeeRequest>
    {
        private readonly AppDbContext _context;

        public CreateEmployeeValidation(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full Name boş ola bilməz")
                .MaximumLength(100).WithMessage("Full Name maksimum 100 simvol ola bilər");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone boş ola bilməz")
                .Must(BeValidPhoneFormat).WithMessage("Phone düzgün formatda olmalıdır (məsələn: +994501234567)")
                .MustAsync(BeUniquePhone).WithMessage("Bu telefon nömrəsi artıq istifadə olunur");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş ola bilməz")
                .EmailAddress().WithMessage("Email düzgün formatda olmalıdır (məsələn: example@mail.com)")
                .MustAsync(BeUniqueEmail).WithMessage("Bu email artıq istifadə olunur");

            RuleFor(x => x.FinCode)
                .NotEmpty().WithMessage("FinCode boş ola bilməz")
                .Length(7).WithMessage("FinCode dəqiq 7 simvol olmalıdır")
                .MustAsync(BeUniqueFinCode).WithMessage("Bu FinCode artıq istifadə olunur");

            RuleFor(x => x.BirthDate)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("Doğum tarixi gələcəkdə ola bilməz")
                .Must(BeAtLeast18YearsOld).WithMessage("Employee ən azı 18 yaşında olmalıdır");

            RuleFor(x => x.HireDate)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Hire Date gələcək tarix ola bilməz");

            RuleFor(x => x.PositionIds)
                .NotEmpty().WithMessage("Ən azı 1 position seçilməlidir")
                .MustAsync(AllPositionsExist).WithMessage("Seçilmiş position mövcud deyil");
        }

        private bool BeValidPhoneFormat(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            var regex = new Regex(@"^\+994\d{9}$"); // AZ format
            return regex.IsMatch(phone);
        }

        private bool BeAtLeast18YearsOld(DateOnly birthDate)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--;
            return age >= 18;
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(email)) return true;
            if (_context == null) return false;
            return !await _context.Employees.AnyAsync(e => e.Email.ToLower() == email.ToLower(), token);
        }

        private async Task<bool> BeUniquePhone(string phone, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(phone)) return true;
            if (_context == null) return false;
            return !await _context.Employees.AnyAsync(e => e.Phone == phone, token);
        }

        private async Task<bool> BeUniqueFinCode(string finCode, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(finCode)) return true;
            if (_context == null) return false;
            return !await _context.Employees.AnyAsync(e => e.FinCode.ToLower() == finCode.ToLower(), token);
        }

        private async Task<bool> AllPositionsExist(List<int> positionIds, CancellationToken token)
        {
            if (_context == null) return false;
            if (positionIds == null || !positionIds.Any()) return false;

            var existing = await _context.Positions
                .Where(p => positionIds.Contains(p.Id))
                .Select(p => p.Id)
                .ToListAsync(token);

            return existing.Count == positionIds.Count;
        }
    }
}
