using AlisRestaurant.Data.Context;

namespace AlisRestaurant.Services.CompanyService;

public class DeleteCompany
{
    private readonly AppDbContext _dbContext;
    public DeleteCompany()
    {
        _dbContext = new AppDbContext();
    }
    public void Execute()
    {
        Console.Clear();
        Console.WriteLine("=== Company Silme ===\n");
        Console.Write("Silmek istediğiniz Company ID'sini girin: ");
        if (!int.TryParse(Console.ReadLine(), out int companyId))
        {
            Console.WriteLine("Tapladi ID . Devam etmek için Enter'a basın...");
            Console.ReadLine();
            return;
        }
        var company = _dbContext.Companies.Find(companyId);
        if (company == null)
        {
            Console.WriteLine("Qeyd edilen ID'ye sahip Company tapilmadi. Devam etmek için Enter'a basın...");
            Console.ReadLine();
            return;
        }
        _dbContext.Companies.Remove(company);
        _dbContext.SaveChanges();
        Console.WriteLine("Company  silindi. Devam etmek için Enter'a basın...");
        Console.ReadLine();
    }
}
