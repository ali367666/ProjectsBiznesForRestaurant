namespace AlisRestaurant.Data.Entities.HR;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Position> Positions { get; set; } = new List<Position>();
}
