namespace AlisRestaurant.DTOs.HRDto.Position;

public class UpdatePositionRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
}
