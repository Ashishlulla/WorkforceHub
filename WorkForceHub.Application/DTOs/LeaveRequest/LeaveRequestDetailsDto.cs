using WorkForceHub.Domain.Enums;

namespace WorkForceHub.Application.DTOs.LeaveRequest
{
    public class LeaveRequestDetailsDto
    {
        public int LeaveRequestId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;

        public LeaveType LeaveType { get; set; } = LeaveType.Casual;

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public string Reason { get; set; } = string.Empty;

        public LeaveStatus Status { get; set; } = LeaveStatus.Pending;

        public int? ApprovedByEmployeeId { get; set; }
        public string ApprovedByEmployeeName { get; set; } = string.Empty;
        public DateTime? ApprovedAt { get; set; }
        public string ApprovalRemarks { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
