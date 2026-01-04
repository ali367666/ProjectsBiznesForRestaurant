namespace AlisRestaurant.DTOs.HRDto.EmployeePosition;

public class ListEmployeePositionRequest
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeFullName { get; set; } = null!;
    public int PositionId { get; set; }
    public string PositionName { get; set; } = null!;
    public DateTime AssignedDate { get; set; }
    public DateTime? EndDate { get; set; }
}
