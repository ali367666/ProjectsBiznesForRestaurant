namespace AlisRestaurant.Data.Entities.HR;

public class EmployeePosition
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public int PositionId { get; set; }
    public Position Position { get; set; } = null!;
    public DateTime AssignedDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; }
}
