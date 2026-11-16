using FluentValidation;
using WorkForceHub.Application.DTOs.LeaveRequest;

namespace WorkForceHub.Application.Validators.LeaveRequest
{
    public class CreateLeaveRequestDtoValidator : AbstractValidator<CreateLeaveRequestDto>
    {
        public CreateLeaveRequestDtoValidator() {

            RuleFor(lr => lr.EmployeeId)
                .GreaterThan(0).WithMessage("EmployeeId must be grater than zero.");

            RuleFor(lr => lr.LeaveType)
                .IsInEnum().WithMessage("Invalid leave type");

            RuleFor(lr => lr.StartDate)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Leave Start date cannot be in past.");

            RuleFor(lr => lr.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("End date cannot be earlier than start date.");

            RuleFor(lr => lr.Reason)
                .NotEmpty().WithMessage("Leave reason is required")
                .MaximumLength(500).WithMessage("Leave reason must not be exceeds 500 characters.");

        }
    }
}
