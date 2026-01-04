using System;
using System.Linq;
using AlisRestaurant.Data.Context;
using AlisRestaurant.DTOs.HRDto.Department;
using AlisRestaurant.Validations;
using AlisRestaurant.Validations.HRValidations.DepartmentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace AlisRestaurant.Services.HrService.DepartmentServices;

public class ListDepartment
{
    private readonly AppDbContext _dbContext;

    public ListDepartment()
    {
        _dbContext = new AppDbContext();
    }

    public void Execute()
    {
        Console.Clear();
        Console.WriteLine("=== Department Siyahısı ===\n");

        var departments = _dbContext.Departments
            .OrderBy(d => d.Id)
            .ToList();

        if (!departments.Any())
        {
            Console.WriteLine("Heç bir Department tapılmadı.");
            Console.ReadLine();
            return;
        }

        foreach (var dept in departments)
        {
            Console.WriteLine($"ID: {dept.Id} | Ad: {dept.Name}");
        }

        Console.WriteLine("\nDavam etmək üçün Enter basın...");
        Console.ReadLine();
    }
}
