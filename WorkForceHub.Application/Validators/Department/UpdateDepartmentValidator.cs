using FluentValidation;
using WorkForceHub.Application.DTOs.Department;

namespace WorkForceHub.Application.Validators.Department
{
    public class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentDto>
    {
        public UpdateDepartmentValidator() {

            RuleFor(d => d.DepartmentId)
                .GreaterThan(0).WithMessage("Department Id is Invalid");

            RuleFor(d => d.DepartmentName)
                .NotEmpty().WithMessage("Department Name is Required")
                .MaximumLength(100).WithMessage("Department Name should not exceeds 100 characters");

            RuleFor(d => d.Description)
                .MaximumLength(500).WithMessage("Department Description should not exceeds 500 characters.");
        }
    }
}
