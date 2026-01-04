namespace AlisRestaurant.DTOs.HRDto.Position;

public class ListPositionRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = null!;
}
