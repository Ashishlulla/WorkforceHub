using WorkForceHub.Domain.Entities;

namespace WorkForceHub.Application.Interfaces.RepositoryInterfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<Employee?> GetEmployeeByCodeAsync(string code);
        Task<List<Employee>> GetAllEmployeesAsync();
        
        
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        
        
        Task DeleteEmployeeByIdAsync(int id);
    }
}
