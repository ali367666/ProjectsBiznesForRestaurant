using System;
using System.Linq;
using AlisRestaurant.Data.Context;
using AlisRestaurant.DTOs.HRDto.Department;
using AlisRestaurant.Validations;
using AlisRestaurant.Validations.HRValidations.DepartmentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace AlisRestaurant.Services.HrService.DepartmentServices;


    public class UpdateDepartment
    {
        private readonly AppDbContext _dbContext;

        public UpdateDepartment()
        {
            _dbContext = new AppDbContext();
        }

        public void Execute()
        {
            Console.Clear();
            Console.WriteLine("=== Department Dəyiş ===");

            Console.Write("Department ID daxil edin: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID düzgün formatda deyil!");
                Console.ReadLine();
                return;
            }

            var department = _dbContext.Departments.FirstOrDefault(d => d.Id == id);
            if (department == null)
            {
                Console.WriteLine("Belə Department mövcud deyil!");
                Console.ReadLine();
                return;
            }

            Console.Write($"Yeni ad daxil edin (köhnə: {department.Name}): ");
            var newName = Console.ReadLine();

            var dto = new UpdateDepartmentRequest
            {
                Id = id,
                Name = newName!
            };

            var validator = new UpdateDepartmentValidation(_dbContext);
            ValidationResult result = validator.Validate(dto);

            if (!result.IsValid)
            {
                Console.WriteLine("\nXətalar:");
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"- {error.ErrorMessage}");
                }
                Console.ReadLine();
                return;
            }

            department.Name = dto.Name;
            _dbContext.SaveChanges();

            Console.WriteLine("\nDepartment uğurla yeniləndi!");
            Console.ReadLine();
        }
    }

