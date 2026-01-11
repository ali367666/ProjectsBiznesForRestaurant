using AlisRestaurant.Data.Entities;
//using AlisRestaurant.Data.Entities.WarehouseAndStock;

namespace AlisRestaurant.DTOs.CompanyDto;

public class ListCompanyRequest
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    
}
