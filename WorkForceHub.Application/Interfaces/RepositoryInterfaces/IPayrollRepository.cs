using WorkForceHub.Domain.Entities;

namespace WorkForceHub.Application.Interfaces.RepositoryInterfaces
{
    public interface IPayrollRepository
    {
        Task<Payroll> AddPayrollAsync(Payroll payroll);

        Task<IEnumerable<Payroll>> GetPayrollsAsync();

        Task<Payroll?> GetPayrollByIdAsync(int id);

        Task<IEnumerable<Payroll>> GetPayrollsByEmployeeIdAsync(int employeeId);

        Task<Payroll> UpdatePayrollAsync(Payroll payroll);

        Task DeletePayrollAsync(Payroll payroll);

        Task<bool> PayrollExistsAsync(int employeeId, int payrollYear, int payRollMonth);
    }
}
