using WorkForceHub.Application.DTOs.Employee;
using WorkForceHub.Application.Interfaces.RepositoryInterfaces;
using WorkForceHub.Application.Interfaces.ServiceInterfaces;

namespace WorkForceHub.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        //Private Feilds
        private readonly IEmployeeRepository _employeeRepository;

        //Constructor
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEmployeeByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeDto?> GetEmployeeByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeDetailsDto?> GetEmployeeDetailsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateEmployeeDto?> GetEmployeeForUpdateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEmployeeAsync(UpdateEmployeeDto updateEmployeeDto)
        {
            throw new NotImplementedException();
        }
    }
}