namespace AlisRestaurant.DTOs.HRDto.Employee;

public class CreateEmployeeRequest
{
    public string FullName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime HireDate { get; set; }
    public bool IsActive { get; set; }
    public string FinCode { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public List<int> PositionIds { get; set; } = new();

    
}
