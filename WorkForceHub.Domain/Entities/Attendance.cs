using WorkForceHub.Domain.Enums;

namespace WorkForceHub.Domain.Entities
{
    public class Attendance
    {
        public int AttendanceId { get; set; }
        public int EmployeeId { get; set; }
        public DateOnly AttendanceDate { get; set; }

        public TimeOnly? LoginTime { get; set; }
        public TimeOnly? LogoutTime { get; set; }
        public AttendanceStatus Status { get; set; } = AttendanceStatus.Present;
        
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        //Navigation Property
        public Employee Employee { get; set; } = null!;
    }
}
