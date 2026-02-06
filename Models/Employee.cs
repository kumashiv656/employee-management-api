namespace EmployeeApi.Models
{
    public class Employee
    {
        public int Id { get; set; }                 // Primary Key

        public string EmployeeCode { get; set; }  = null!;  // EMP001, EMP002

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Department { get; set; } = null!;

        public decimal Salary { get; set; }

        public DateTime DateOfJoining { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; } =DateTime.UtcNow;
    }
}
