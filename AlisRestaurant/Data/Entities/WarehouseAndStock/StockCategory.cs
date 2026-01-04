namespace AlisRestaurant.Data.Entities.WarehouseAndStock;

public class StockCategory
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int? ParentId { get; set; }
    public StockCategory Parent { get; set; }

    public ICollection<StockCategory> Children { get; set; } = new List<StockCategory>();

    public ICollection<StockItem> StockItems { get; set; } = new List<StockItem>();
}
