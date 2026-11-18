using FluentValidation;
using WorkForceHub.Application.DTOs.Notifications;

namespace WorkForceHub.Application.Validators.Notifications
{
    public class CreateNotificationDtoValidator : AbstractValidator<CreateNotificationDto>
    {
        public CreateNotificationDtoValidator() {

            RuleFor(n => n.EmployeeId)
                .GreaterThan(0).WithMessage("Employee is required");

            RuleFor(n => n.Title)
                .NotEmpty().WithMessage("Notification title is required.")
                .MaximumLength(100).WithMessage("Title must not exceeds 100 characters");

            RuleFor(n => n.Message)
                .NotEmpty().WithMessage("Notification message is required")
                .MaximumLength(500).WithMessage("Notification message must not exceeds 500 characters");

            RuleFor(n => n.Type)
                .IsInEnum().WithMessage("Invalid notification type");
        }
    }
}
