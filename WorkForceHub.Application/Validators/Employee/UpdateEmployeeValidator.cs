using FluentValidation;
using WorkForceHub.Application.DTOs.Employee;
using WorkForceHub.Domain.Enums;

namespace WorkForceHub.Application.Validators.Employee
{
    public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeDto>
    {
        public UpdateEmployeeValidator()
        {
            RuleFor(e => e.EmployeeId)
                .NotEmpty().WithMessage("Employee ID is required");
            RuleFor(e => e.EmployeeCode)
                .NotEmpty().WithMessage("Employee code is required");

            RuleFor(e => e.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(20).WithMessage("First name cannot exceed 20 characters");

            RuleFor(e => e.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(20).WithMessage("Last name cannot exceed 20 characters");

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");


            RuleFor(e => e.Phone)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^[0-9]{10}$")
                .WithMessage("Phone number must contain exactly 10 digits.");

            RuleFor(e => e.DateOfBirth)
                 .NotEmpty().WithMessage("Date of birth is required")
                 .LessThan(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Date of birth cannot be in the future");

            RuleFor(e => e.Gender)
                .NotEmpty().WithMessage("Gender is required")
                .Must(g => g == GenderType.Male || g == GenderType.Female || g == GenderType.Other).WithMessage("Invalid gender value");

            RuleFor(e => e.JobTitle)
                .NotEmpty().WithMessage("Job title is required");

            RuleFor(e => e.DepartmentId)
             .GreaterThan(0)
             .WithMessage("Department is required");

            RuleFor(e => e.ShiftId)
                .GreaterThan(0)
                .WithMessage("Shift is required");

            RuleFor(e => e.DateOfJoining)
                 .GreaterThanOrEqualTo(e => e.DateOfBirth)
                 .WithMessage("Date of joining cannot be before date of birth");

            RuleFor(e => e.EmploymentType)
                .NotEmpty().WithMessage("Employment type is required")
                .Must(et => et == EmploymentType.FullTime || et == EmploymentType.PartTime || et == EmploymentType.Contract).WithMessage("Invalid employment type value");

            RuleFor(e => e.Status)
                .NotEmpty().WithMessage("Status is required")
                .Must(s => s == EmployeeStatus.Active || s == EmployeeStatus.Inactive || s == EmployeeStatus.Terminated || s == EmployeeStatus.OnLeave || s == EmployeeStatus.Resigned).WithMessage("Invalid status value");

            RuleFor(e => e.Address)
                .NotEmpty().WithMessage("Address is required");

            RuleFor(e => e.City)
                .NotEmpty().WithMessage("City is required");

            RuleFor(e => e.State)
                .NotEmpty().WithMessage("State is required");

            RuleFor(e => e.PostalCode)
                .NotEmpty().WithMessage("Postal code is required")
                .Matches(@"^[0-9]{6}$").WithMessage("Invalid postal code format");

        }
    }
}