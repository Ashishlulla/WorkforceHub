using WorkForceHub.Application.DTOs.Employee;

namespace WorkForceHub.Application.Interfaces.ServiceInterfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto> GetEmployeeByIdAsync(int id);
        Task<EmployeeDetailsDto> GetEmployeeDetailsByIdAsync(int id);
        Task<UpdateEmployeeDto> GetEmployeeForUpdateAsync(int id);
        Task<EmployeeDto> GetEmployeeByCodeAsync(string code);

        
        Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto);
        Task<EmployeeDto> UpdateEmployeeAsync(UpdateEmployeeDto updateEmployeeDto);
        Task DeleteEmployeeByIdAsync(int id);
    }
}
