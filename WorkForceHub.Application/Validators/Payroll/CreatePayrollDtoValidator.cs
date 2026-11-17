using FluentValidation;
using WorkForceHub.Application.DTOs.Payroll;

namespace WorkForceHub.Application.Validators.Payroll
{
    public class CreatePayrollDtoValidator : AbstractValidator<CreatePayrollDto>
    {
        public CreatePayrollDtoValidator() {

            RuleFor(p => p.EmployeeId)
                .GreaterThan(0).WithMessage("Employee ID must be greater than zero.");

            RuleFor(p => p.PayrollYear)
                .InclusiveBetween(2000, DateTime.UtcNow.Year + 1)
                .WithMessage("Payroll year must be valid.");


            RuleFor(p => p.PayrollMonth)
                .InclusiveBetween(1, 12).WithMessage("Payroll Month must be between 1 to 12");

            RuleFor(p => p.BasicSalary)
                .GreaterThan(0).WithMessage("Basic salary  must be greater than 0");

            RuleFor(p => p.Allowances)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Allowances cannot be negative.");

            RuleFor(p => p.Deductions)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Deductions cannot be negative.");

            RuleFor(p => p.Deductions)
                .LessThanOrEqualTo(p => p.BasicSalary + p.Allowances)
                .WithMessage("Deductions cannot exceed gross salary.");
        }
    }
}
