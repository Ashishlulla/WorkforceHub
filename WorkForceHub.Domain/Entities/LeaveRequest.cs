using WorkForceHub.Domain.Enums;

namespace WorkForceHub.Domain.Entities
{
    public class LeaveRequest
    {
        public int LeaveRequestId { get; set; }
        public int EmployeeId { get; set; }

        public LeaveType LeaveType { get; set; } = LeaveType.Casual;
        
        public DateOnly StartDate { get; set; }      
        public DateOnly EndDate { get; set; }
        
        public string Reason { get; set; } = string.Empty;

        public LeaveStatus Status { get; set; } = LeaveStatus.Pending;

        public int? ApprovedByEmployeeId { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string ApprovalRemarks { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        //Navigation Property

        public Employee Employee { get; set; } = null!;
    }
}
