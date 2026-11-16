using FluentValidation;
using WorkForceHub.Application.DTOs.LeaveRequest;
using WorkForceHub.Domain.Enums;

namespace WorkForceHub.Application.Validators.LeaveRequest
{
    public class UpdateLeaveRequestStatusDtoValidator :AbstractValidator<UpdateLeaveRequestStatusDto>
    {
        public UpdateLeaveRequestStatusDtoValidator() {

            RuleFor(lr => lr.Status)
                .Must(status =>
                status == LeaveStatus.Approved ||
                status == LeaveStatus.Rejected)
                .WithMessage("status must be approved or rejected");

            RuleFor(lr => lr.ApprovedByEmployeeId)
                .GreaterThan(0).WithMessage("Approver EmployeeId must be greater than zero.");

            RuleFor(lr => lr.ApprovalRemarks)
                .NotEmpty().WithMessage("Approved remarks cannot be empty.")
                .MaximumLength(500).WithMessage("Approval remark cannot exceed 500 characters.");
        }
    }
}
