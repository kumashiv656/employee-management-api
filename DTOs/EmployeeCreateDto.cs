namespace EmployeeApi.DTOs
{
    public class EmployeeCreateDto
    {
        public string EmployeeCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Department { get; set; }

        public decimal Salary { get; set; }

        public string Email { get; set; }
    }
}