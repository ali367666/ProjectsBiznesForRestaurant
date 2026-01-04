namespace AlisRestaurant.Data.Entities.HR;

public class Employee
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;

    public DateTime HireDate { get; set; }
    public bool IsActive { get; set; }
    public string FinCode { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public ICollection<EmployeePosition> EmployeePositions { get; set; } = new List<EmployeePosition>();
}

