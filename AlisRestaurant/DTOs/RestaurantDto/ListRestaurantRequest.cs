using AlisRestaurant.Data.Entities;
//using AlisRestaurant.Data.Entities.WarehouseAndStock;

namespace AlisRestaurant.DTOs.RestaurantDto;

public  class ListRestaurantRequest
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    //public ICollection<WareHouse> Warehouses { get; set; }
}
