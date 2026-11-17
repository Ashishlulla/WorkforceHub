using FluentValidation;
using WorkForceHub.Application.DTOs.Payroll;
using WorkForceHub.Domain.Enums;

namespace WorkForceHub.Application.Validators.Payroll
{
    public class UpdatePayrollStatusDtoValidator :AbstractValidator<UpdatePayrollStatusDto>
    {
        public UpdatePayrollStatusDtoValidator() {

            RuleFor(p => p.Status)
                .IsInEnum()
                .Must(status => status == PayrollStatus.Paid || status == PayrollStatus.Rejected).WithMessage("Payroll status must be Paid or Rejected.");
        }
    }
}
