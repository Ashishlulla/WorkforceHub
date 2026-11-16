using WorkForceHub.Domain.Enums;

namespace WorkForceHub.Application.DTOs.LeaveRequest
{
    public class UpdateLeaveRequestStatusDto
    {
        public LeaveStatus Status { get; set; }
        public int ApprovedByEmployeeId { get; set; }
        public string ApprovalRemarks { get; set; } = string.Empty;
    }
}
