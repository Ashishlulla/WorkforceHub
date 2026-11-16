using WorkForceHub.Domain.Enums;

namespace WorkForceHub.Application.DTOs.LeaveRequest
{
    public class CreateLeaveRequestDto
    {
        public int EmployeeId { get; set; }

        public LeaveType LeaveType { get; set; } = LeaveType.Casual;

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public string Reason { get; set; } = string.Empty;
    }
}
