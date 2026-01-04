namespace AlisRestaurant.Data.Entities.WarehouseAndStock;

public class StockItem
{
    public int Id { get; set; }
    public string Name { get; set; }//Kola,Kartof,Et
    public string Description { get; set; }//Kola 2L Zero,Et 250 qramliq
    public int CategoryId { get; set; }//Gida,Icecek
    public StockCategory Category { get; set; }
    public int UnitId { get; set; }
    public Unit Unit { get; set; }
}
