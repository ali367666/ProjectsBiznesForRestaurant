using System;
using System.Linq;
using AlisRestaurant.Data.Context;
using AlisRestaurant.DTOs.HRDto.Department;
using AlisRestaurant.Validations;
using AlisRestaurant.Validations.HRValidations.DepartmentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace AlisRestaurant.Services.HrService.DepartmentServices;


    public class DeleteDepartment
    {
        private readonly AppDbContext _dbContext;

        public DeleteDepartment()
        {
            _dbContext = new AppDbContext();
        }

        public void Execute()
        {
            Console.Clear();
            Console.WriteLine("=== Department Sil ===");

            Console.Write("Silmək istədiyiniz Department ID daxil edin: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID düzgün deyil!");
                Console.ReadLine();
                return;
            }

            var department = _dbContext.Departments
                .Include(d => d.Positions)
                .FirstOrDefault(d => d.Id == id);

            if (department == null)
            {
                Console.WriteLine("Belə Department mövcud deyil!");
                Console.ReadLine();
                return;
            }

            if (department.Positions.Any())
            {
                Console.WriteLine("Bu Department digər cədvəllərdə istifadə olunur, silinə bilməz!");
                Console.ReadLine();
                return;
            }

            var dto = new DeleteDepartmentRequest { Id = id };
            var validator = new DeleteDepartmentValidation(_dbContext);
            ValidationResult result = validator.Validate(dto);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                    Console.WriteLine($"Xəta: {error.ErrorMessage}");
                Console.ReadLine();
                return;
            }

            _dbContext.Departments.Remove(department);
            _dbContext.SaveChanges();

            Console.WriteLine("Department uğurla silindi!");
            Console.ReadLine();
        }
    }

