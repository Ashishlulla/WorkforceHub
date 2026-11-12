using FluentValidation;
using WorkForceHub.Application.DTOs.Attendance;

namespace WorkForceHub.Application.Validators.Attendance
{
    public class UpdateAttendanceDtoValidator : AbstractValidator<UpdateAttendanceDto>
    {
        public UpdateAttendanceDtoValidator() {

            RuleFor(a => a.AttendanceId)
               .GreaterThan(0).WithMessage("AttendanceId must be greater than zero");

            RuleFor(a => a.EmployeeId)
               .GreaterThan(0).WithMessage("EmployeeId must be greater than zero");

            RuleFor(a => a.AttendanceDate)
                .Equal(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Attendance can be only recorded for current date");

            RuleFor(a => a.LoginTime)
                .Must((dto, loginTime) => !loginTime.HasValue || !dto.LogoutTime.HasValue || loginTime.Value < dto.LogoutTime.Value).WithMessage("Login time must be earlier than Logout time.");

            RuleFor(a => a.Status)
                .IsInEnum().WithMessage("Invalid attendance status.");
        }
    }
}
