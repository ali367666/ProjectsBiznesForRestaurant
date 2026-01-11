using AlisRestaurant.Data.Entities;
//using AlisRestaurant.Data.Entities.WarehouseAndStock;

namespace AlisRestaurant.DTOs.RestaurantDto;

public class CreateRestaurantRequest
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public int CompanyId { get; set; }
    
}
