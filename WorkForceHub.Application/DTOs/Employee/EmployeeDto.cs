using WorkForceHub.Domain.Enums;

namespace WorkForceHub.Application.DTOs.Employee
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }

        public string EmployeeCode { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public DateOnly DateOfBirth { get; set; }

        public GenderType Gender { get; set; }

        public string JobTitle { get; set; } = string.Empty;

        public int DepartmentId { get; set; }

        public int ShiftId { get; set; }

        public DateOnly DateOfJoining { get; set; }

        public EmploymentType EmploymentType { get; set; } = EmploymentType.FullTime;

        public EmployeeStatus Status { get; set; } = EmployeeStatus.Active;
    }
}
