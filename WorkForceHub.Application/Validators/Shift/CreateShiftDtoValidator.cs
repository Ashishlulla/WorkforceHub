using FluentValidation;
using WorkForceHub.Application.DTOs.Shift;


namespace WorkForceHub.Application.Validators.Shift
{
    public class CreateShiftDtoValidator : AbstractValidator<CreateShiftDto>
    {
        public CreateShiftDtoValidator() {

            RuleFor(s => s.ShiftType)
                .IsInEnum().WithMessage("Invalid shift type");

            RuleFor(s => s.StartTime)
                .GreaterThanOrEqualTo(TimeSpan.Zero)
                .LessThan(TimeSpan.FromDays(1)).WithMessage("Start time must be within Valid day.");

            RuleFor(s => s.EndTime)
                .GreaterThanOrEqualTo(TimeSpan.Zero)
                .LessThan(TimeSpan.FromDays(1)).WithMessage("End time must be within Valid day.");

            RuleFor(s => s.GracePeriodInMinutes)
                .GreaterThanOrEqualTo(0).WithMessage("Grace peroid cannot be in negative");

            RuleFor(s => s)
                .Must(s => s.IsOvernight ? s.EndTime < s.StartTime : s.EndTime > s.StartTime).WithMessage("shift timing do not match overnight setting.");

        }
    }
}
