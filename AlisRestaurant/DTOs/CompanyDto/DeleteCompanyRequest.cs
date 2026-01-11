using AlisRestaurant.Data.Entities;
//using AlisRestaurant.Data.Entities.WarehouseAndStock;

namespace AlisRestaurant.DTOs.CompanyDto;

public class DeleteCompanyRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
}
