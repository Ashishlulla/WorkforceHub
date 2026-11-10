using Microsoft.AspNetCore.Mvc;
using WorkForceHub.Application.DTOs.Employee;
using WorkForceHub.Application.Interfaces.ServiceInterfaces;

namespace WorkForceHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        //Private Feilds
        private readonly IEmployeeService _employeeService;

        //Constructor
        public EmployeeController(IEmployeeService employeeService)
        {

            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {

            var employees = await _employeeService.GetAllEmployeesAsync();

            return Ok(employees);
        }

        [HttpGet("code/{code}")]
        public async Task<IActionResult> GetEmployeeByCodeAsync(string code)
        {

            var employee = await _employeeService.GetEmployeeByCodeAsync(code);

            return Ok(employee);
        }

        [HttpGet("id/{id:int}")]
        public async Task<IActionResult> GetEmployeeByIdAsync(int id)
        {

            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            return Ok(employee);
        }

        [HttpGet("{id:int}/details")]
        public async Task<IActionResult> GetEmployeeDetailsByIdAsync(int id)
        {

            var employee = await _employeeService.GetEmployeeDetailsByIdAsync(id);

            return Ok(employee);
        }

        [HttpGet("{id:int}/edit")]
        public async Task<IActionResult> GetEmployeeForUpdateByIdAsync(int id)
        {

            var employee = await _employeeService.GetEmployeeForUpdateAsync(id);

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeAsync([FromBody] CreateEmployeeDto createEmployeeDto)
        {

            var employee = await _employeeService.CreateEmployeeAsync(createEmployeeDto);

            return StatusCode(201, employee);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody]UpdateEmployeeDto updateEmployeeDto) 
        {
            if (id != updateEmployeeDto.EmployeeId)
            {
                return BadRequest("Employee ID in URL does not match request body.");
            }

            var updatedEmployee = await _employeeService.UpdateEmployeeAsync(updateEmployeeDto);

            return Ok(updatedEmployee);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id) {

            await _employeeService.DeleteEmployeeByIdAsync(id);

            return NoContent();
        }
    }
}
