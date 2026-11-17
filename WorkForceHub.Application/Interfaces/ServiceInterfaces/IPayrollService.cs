using WorkForceHub.Application.DTOs.Payroll;

namespace WorkForceHub.Application.Interfaces.ServiceInterfaces
{
    public interface IPayrollService
    {
        Task<PayrollDto> CreatePayrollAsync(CreatePayrollDto createPayrollDto);
        Task<IEnumerable<PayrollDto>> GetPayrollsAsync();

        Task<PayrollDto> GetPayrollByIdAsync(int id);
        
        Task<IEnumerable<PayrollDto>> GetPayrollByEmployeeIdAsync(int employeeId);

        Task<PayrollDetailsDto> GetPayrollDetailsAsync(int id);

        Task<PayrollDetailsDto> UpdatePayrollStatusAsync(int id, UpdatePayrollStatusDto updatePayrollStatusDto);

        Task DeletePayrollAsync(int id);
    }
}
