using AlisRestaurant.Data.Entities;
//using AlisRestaurant.Data.Entities.WarehouseAndStock;

namespace AlisRestaurant.DTOs.RestaurantDto;

public class DeleteRestaurantRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    
}
