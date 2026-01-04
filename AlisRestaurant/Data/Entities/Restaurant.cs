using AlisRestaurant.Data.Entities.WarehouseAndStock;

namespace AlisRestaurant.Data.Entities;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public ICollection<WareHouse> Warehouses { get; set; }
}
