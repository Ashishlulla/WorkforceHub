using WorkForceHub.Domain.Enums;

namespace WorkForceHub.Application.DTOs.Department
{
    public class DepartmentDetailsDto
    {
        // Department Information
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Employees
        public List<EmployeeInfo> Employees { get; set; } = new();

        public class EmployeeInfo
        {
            public int EmployeeId { get; set; }
            public string EmployeeCode { get; set; } = string.Empty;
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string JobTitle { get; set; } = string.Empty;
            public EmployeeStatus Status { get; set; }
        }
    }
}