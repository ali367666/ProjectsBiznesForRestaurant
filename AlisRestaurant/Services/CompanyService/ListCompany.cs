
using System;
using System.Linq;
using AlisRestaurant.Data.Context;
using AlisRestaurant.DTOs.HRDto.Department;
using AlisRestaurant.Validations;
using AlisRestaurant.Validations.HRValidations.DepartmentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace AlisRestaurant.Services.CompanyService;

public class ListCompany
{
    private readonly AppDbContext _dbContext;

    public ListCompany()
    {
        _dbContext = new AppDbContext();
    }

    public void Execute()
    {
        Console.Clear();
        Console.WriteLine("=== Company Siyahısı ===\n");

        var companies = _dbContext.Companies
            .OrderBy(d => d.Id)
            .ToList();

        if (!companies.Any())
        {
            Console.WriteLine("Heç bir Company tapılmadı.");
            Console.ReadLine();
            return;
        }

        foreach (var comp in companies)
        {
            Console.WriteLine($"ID: {comp.Id} | Ad: {comp.Name} | Address: {comp.Address} | Email: {comp.Email}");
        }

        Console.WriteLine("\nDavam etmək üçün Enter basın...");
        Console.ReadLine();
    }
}
