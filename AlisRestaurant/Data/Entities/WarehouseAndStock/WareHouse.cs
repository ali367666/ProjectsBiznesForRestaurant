using AlisRestaurant.Enum;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AlisRestaurant.Data.Entities.WarehouseAndStock;

public class WareHouse
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public Company? Company { get; set; }
    public int RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }
    public ICollection<WareHouseStock> ?Stocks { get; set; }
    public WarehouseType Type { get; set; }
}
