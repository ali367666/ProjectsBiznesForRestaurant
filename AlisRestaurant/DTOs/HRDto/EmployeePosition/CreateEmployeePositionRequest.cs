namespace AlisRestaurant.DTOs.HRDto.EmployeePosition;

public class CreateEmployeePositionRequest
{
    public int EmployeeId { get; set; }       
    public int PositionId { get; set; }        
    public DateTime AssignedDate { get; set; }
}
