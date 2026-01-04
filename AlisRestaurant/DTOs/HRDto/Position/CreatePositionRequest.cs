namespace AlisRestaurant.DTOs.HRDto.Position;

public class CreatePositionRequest
{
    public string Name { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
}
