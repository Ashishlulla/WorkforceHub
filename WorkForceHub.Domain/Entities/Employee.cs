using WorkForceHub.Domain.Enums;

namespace WorkForceHub.Domain.Entities
{
    public class Employee
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

        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        //Navigation properties

        public Department Department { get; set; } = null!;
        public Shift Shift { get; set; } = null!;
    }
}
