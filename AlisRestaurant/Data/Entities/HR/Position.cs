namespace AlisRestaurant.Data.Entities.HR;

public class Position
{

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        public ICollection<EmployeePosition> EmployeePositions { get; set; } = new List<EmployeePosition>();


}
