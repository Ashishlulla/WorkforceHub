using WorkForceHub.Domain.Entities;

namespace WorkForceHub.Application.Interfaces.RepositoryInterfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<Employee?> GetEmployeeByCodeAsync(string code);
        Task<List<Employee>> GetAllEmployeesAsync();
        
        
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<Employee> UpdateEmployeeAsync(Employee employee);
        
        
        Task DeleteEmployeeByIdAsync(int id);
    }
}
