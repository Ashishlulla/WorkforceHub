using WorkForceHub.Domain.Enums;

namespace WorkForceHub.Application.DTOs.Attendance
{
    public class AttendanceDetailsDto
    {
        public int AttendanceId { get; set; }
        public int EmployeeId { get; set; }

        public string EmployeeCode { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;

        public DateOnly AttendanceDate { get; set; }

        public TimeOnly? LoginTime { get; set; }
        public TimeOnly? LogoutTime { get; set; }
        public AttendanceStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
