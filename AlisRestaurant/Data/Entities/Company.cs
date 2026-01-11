//using AlisRestaurant.Data.Entities.WarehouseAndStock;

namespace AlisRestaurant.Data.Entities;

public class Company
{
    public int Id { get; set; }
    public string? Name { get; set; } = null!;
    public string? Address { get; set; } = null!;
    public string? PhoneNumber { get; set; } = null!;
    public string? Email { get; set; } = null!;
    //public WareHouse CentralWarehouse { get; set; }
    public ICollection<Restaurant> ?Restaurants { get; set; }
}
