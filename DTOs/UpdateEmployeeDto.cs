namespace EmployeeManagementSystem.DTOs
{
    public class UpdateEmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public decimal Salary { get; set; }
        public int Experience { get; set; }
    }
}
