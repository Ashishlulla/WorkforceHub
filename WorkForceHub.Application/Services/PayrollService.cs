using AutoMapper;
using FluentValidation;
using WorkForceHub.Application.DTOs.Payroll;
using WorkForceHub.Application.Interfaces.RepositoryInterfaces;
using WorkForceHub.Application.Interfaces.ServiceInterfaces;
using WorkForceHub.Domain.Entities;
using WorkForceHub.Domain.Enums;

namespace WorkForceHub.Application.Services
{
    public class PayrollService : IPayrollService
    {
        // Private Fields
        private readonly IPayrollRepository _payrollRepository;
        private readonly IEmployeeRepository _employeeRepository;

        private readonly IMapper _mapper;

        private readonly IValidator<CreatePayrollDto> _createPayrollDtoValidator;
        private readonly IValidator<UpdatePayrollStatusDto> _updatePayrollStatusDtoValidator;

        // Constructor
        public PayrollService(
            IPayrollRepository payrollRepository,
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            IValidator<CreatePayrollDto> createPayrollDtoValidator,
            IValidator<UpdatePayrollStatusDto> updatePayrollStatusDtoValidator)
        {
            _payrollRepository = payrollRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _createPayrollDtoValidator = createPayrollDtoValidator;
            _updatePayrollStatusDtoValidator = updatePayrollStatusDtoValidator;
        }

        public async Task<PayrollDto> CreatePayrollAsync(CreatePayrollDto createPayrollDto)
        {
            await _createPayrollDtoValidator.ValidateAndThrowAsync(createPayrollDto);

            Employee employee = await EnsureEmployeeExistsAsync(createPayrollDto.EmployeeId);

            if (await _payrollRepository.PayrollExistsAsync(
                createPayrollDto.EmployeeId,
                createPayrollDto.PayrollYear,
                createPayrollDto.PayrollMonth))
            {
                throw new InvalidOperationException(
                    $"Payroll already exists for Employee ID: {createPayrollDto.EmployeeId}, " +
                    $"Year: {createPayrollDto.PayrollYear}, " +
                    $"Month: {createPayrollDto.PayrollMonth}.");
            }

            Payroll payroll = _mapper.Map<Payroll>(createPayrollDto);

            payroll.Employee = employee;
            payroll.GrossSalary = payroll.BasicSalary + payroll.Allowances;
            payroll.NetSalary = payroll.GrossSalary - payroll.Deductions;
            payroll.Status = PayrollStatus.Pending;
            payroll.CreatedAt = DateTime.UtcNow;

            Payroll addedPayroll = await _payrollRepository.AddPayrollAsync(payroll);

            return _mapper.Map<PayrollDto>(addedPayroll);
        }

        public async Task DeletePayrollAsync(int id)
        {
            Payroll payroll = await EnsurePayrollExistsAsync(id);

            await _payrollRepository.DeletePayrollAsync(payroll);
        }

        public async Task<IEnumerable<PayrollDto>> GetPayrollByEmployeeIdAsync(int employeeId)
        {
            await EnsureEmployeeExistsAsync(employeeId);

            IEnumerable<Payroll> payrolls =
                await _payrollRepository.GetPayrollsByEmployeeIdAsync(employeeId);

            return _mapper.Map<IEnumerable<PayrollDto>>(payrolls);
        }

        public async Task<PayrollDto> GetPayrollByIdAsync(int id)
        {
            Payroll payroll = await EnsurePayrollExistsAsync(id);

            return _mapper.Map<PayrollDto>(payroll);
        }

        public async Task<PayrollDetailsDto> GetPayrollDetailsAsync(int id)
        {
            Payroll payroll = await EnsurePayrollExistsAsync(id);

            return _mapper.Map<PayrollDetailsDto>(payroll);
        }

        public async Task<IEnumerable<PayrollDto>> GetPayrollsAsync()
        {
            IEnumerable<Payroll> payrolls =
                await _payrollRepository.GetPayrollsAsync();

            return _mapper.Map<IEnumerable<PayrollDto>>(payrolls);
        }

        public async Task<PayrollDetailsDto> UpdatePayrollStatusAsync(
            int id,
            UpdatePayrollStatusDto updatePayrollStatusDto)
        {
            await _updatePayrollStatusDtoValidator.ValidateAndThrowAsync(
                updatePayrollStatusDto);

            Payroll payroll = await EnsurePayrollExistsAsync(id);

            if (payroll.Status != PayrollStatus.Pending)
            {
                throw new InvalidOperationException(
                    "Only pending payrolls can have their status updated.");
            }

            _mapper.Map(updatePayrollStatusDto, payroll);

            if (payroll.Status == PayrollStatus.Paid)
            {
                payroll.PaymentDate = DateTime.UtcNow;
            }
            else if (payroll.Status == PayrollStatus.Rejected)
            {
                payroll.PaymentDate = null;
            }

            payroll.UpdatedAt = DateTime.UtcNow;

            Payroll updatedPayroll =
                await _payrollRepository.UpdatePayrollAsync(payroll);

            return _mapper.Map<PayrollDetailsDto>(updatedPayroll);
        }

        #region Helper Methods

        private async Task<Employee> EnsureEmployeeExistsAsync(int employeeId)
        {
            Employee? employee =
                await _employeeRepository.GetEmployeeByIdAsync(employeeId);

            if (employee == null)
            {
                throw new KeyNotFoundException(
                    $"No employee found with ID: {employeeId}");
            }

            return employee;
        }

        private async Task<Payroll> EnsurePayrollExistsAsync(int payrollId)
        {
            Payroll? payroll =
                await _payrollRepository.GetPayrollByIdAsync(payrollId);

            if (payroll == null)
            {
                throw new KeyNotFoundException(
                    $"No payroll found with ID: {payrollId}");
            }

            return payroll;
        }

        #endregion
    }
}
