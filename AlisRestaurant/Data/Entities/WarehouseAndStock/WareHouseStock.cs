namespace AlisRestaurant.Data.Entities.WarehouseAndStock;

public class WareHouseStock
{
    public int Id { get; set; }

    public int WarehouseId { get; set; }
    public WareHouse Warehouse { get; set; }

    public int StockItemId { get; set; }
    public StockItem StockItem { get; set; }

    public decimal Quantity { get; set; }
    public int UnitId { get; set; }
    public Unit Unit { get; set; }
}