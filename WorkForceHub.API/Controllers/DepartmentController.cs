using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkForceHub.Application.DTOs.Department;
using WorkForceHub.Application.Interfaces.ServiceInterfaces;
using WorkForceHub.Domain.Entities;

namespace WorkForceHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        //Private Fields
        private readonly IDepartmentService _departmentService;

        //Constructor
        public DepartmentController(IDepartmentService departmentService) {

            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartmentsAsync() {

            var departments = await _departmentService.GetDepartmentsAsync();

            return Ok(departments);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDepartmentByIdAsync(int id) {

            var department = await _departmentService.GetDepartmentByIdAsync(id);

            return Ok(department);
        }

        [HttpGet("details/{id:int}")]
        public async Task<IActionResult> GetDepartmentDetailsAsync(int id) {

            DepartmentDetailsDto department = await _departmentService.GetDepartmentDetailsAsync(id);

            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartmentAsync([FromBody]CreateDepartmentDto createDepartmentDto) {

            var department = await _departmentService.CreateDepartmentAsync(createDepartmentDto);

            return StatusCode(201, department);
        }

        [HttpPut("edit/{id:int}")]
        public async Task<IActionResult> UpdateDepartmentAsync(int id , [FromBody]UpdateDepartmentDto updateDepartmentDto) {

            if (id != updateDepartmentDto.DepartmentId) {

                return BadRequest("Route Id and Department Id do not match");
            }

            var updatedDepartment = await _departmentService.UpdateDepartmentAsync(id, updateDepartmentDto);

            return Ok(updatedDepartment);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDepartmentAsync(int id) {

            try
            {
                await _departmentService.DeleteDepartmentByIdAsync(id);

                return NoContent();

            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex) {

                return Conflict(new { message = ex.Message });
            }
        }
    }
}
