using AutoMapper;
using FluentValidation;
using WorkForceHub.Application.DTOs.Employee;
using WorkForceHub.Application.Interfaces.RepositoryInterfaces;
using WorkForceHub.Application.Interfaces.ServiceInterfaces;
using WorkForceHub.Domain.Entities;

namespace WorkForceHub.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        //Private Feilds
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateEmployeeDto> _createEmployeeValidator;

        private readonly IValidator<UpdateEmployeeDto> _updateEmployeeValidator;


        //Constructor
        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, IValidator<CreateEmployeeDto> createEmployeeValidator, IValidator<UpdateEmployeeDto> updateEmployeeValidator)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _createEmployeeValidator = createEmployeeValidator;
            _updateEmployeeValidator = updateEmployeeValidator;
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto)
        {
            await _createEmployeeValidator.ValidateAndThrowAsync(createEmployeeDto);

            await ValidateEmployeeCodeIsUniqueAsync(null, createEmployeeDto.EmployeeCode);
            
            var employee = _mapper.Map<Employee>(createEmployeeDto);
            employee.CreatedAt = DateTime.UtcNow;
            var createdEmployee = await _employeeRepository.AddEmployeeAsync(employee);

            return _mapper.Map<EmployeeDto>(createdEmployee);
        }

        public async  Task DeleteEmployeeByIdAsync(int id)
        {
            var employee = await GetEmployeeOrThrowAsync(id);

            await _employeeRepository.DeleteEmployeeByIdAsync(employee);
            
        }

        public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
             var employees = await _employeeRepository.GetAllEmployeesAsync();
             return _mapper.Map<List<EmployeeDto>>(employees); 
        }

        public async Task<EmployeeDto> GetEmployeeByCodeAsync(string code)
        {
            var employee = await _employeeRepository.GetEmployeeByCodeAsync(code);
            if (employee == null) 
            {
                throw new KeyNotFoundException($"Employee with code {code} not found.");
            }
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var employee = await GetEmployeeOrThrowAsync(id);

            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDetailsDto> GetEmployeeDetailsByIdAsync(int id)
        {
            var employee = await GetEmployeeOrThrowAsync(id);

            return _mapper.Map<EmployeeDetailsDto>(employee);
        }

        public async Task<UpdateEmployeeDto> GetEmployeeForUpdateAsync(int id)
        {
            var employee = await GetEmployeeOrThrowAsync(id);
                
            return _mapper.Map<UpdateEmployeeDto>(employee);
        }

        public async Task<EmployeeDto> UpdateEmployeeAsync(int id, UpdateEmployeeDto updateEmployeeDto)
        {

            if (id != updateEmployeeDto.EmployeeId)
            {

                throw new ArgumentException($"Route Id do not match user passed Id.");
            }

            await _updateEmployeeValidator.ValidateAndThrowAsync(updateEmployeeDto);

            Employee employee = await GetEmployeeOrThrowAsync(updateEmployeeDto.EmployeeId);

            await ValidateEmployeeCodeIsUniqueAsync(updateEmployeeDto.EmployeeId, updateEmployeeDto.EmployeeCode);

            _mapper.Map(updateEmployeeDto, employee);

            var updatedEmployee = await _employeeRepository.UpdateEmployeeAsync(employee);

            return _mapper.Map<EmployeeDto>(updatedEmployee);

        }

        #region HelperMethods
        private async Task<Employee> GetEmployeeOrThrowAsync(int id) {

            Employee? employee = await _employeeRepository.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                throw new KeyNotFoundException($"No employee found with ID: {id}");
            }

            return employee;
        }

        private async Task ValidateEmployeeCodeIsUniqueAsync(int? employeeId, string employeeCode) {

            Employee? employee = await _employeeRepository.GetEmployeeByCodeAsync(employeeCode);
            if (employee != null && employee.EmployeeId != employeeId) {

                throw new ValidationException($"Employee Code: {employeeCode} already exists");
            }
        }

        #endregion
    }
}