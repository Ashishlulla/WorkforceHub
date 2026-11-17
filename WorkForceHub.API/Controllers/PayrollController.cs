using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkForceHub.Application.DTOs.Payroll;
using WorkForceHub.Application.Interfaces.ServiceInterfaces;

namespace WorkForceHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        // Private Fields
        private readonly IPayrollService _payrollService;

        // Constructor
        public PayrollController(IPayrollService payrollService){

            _payrollService = payrollService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPayrollsAsync() {

            IEnumerable<PayrollDto> payrolls = await _payrollService.GetPayrollsAsync();

            return Ok(payrolls);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPayrollByIdAsync(int id) {

            PayrollDto payroll = await _payrollService.GetPayrollByIdAsync(id);

            return Ok(payroll);
        }

        [HttpGet("details/{id:int}")]
        public async Task<IActionResult> GetPayrollDetailsAsync(int id) {

            PayrollDetailsDto payrollDetails = await _payrollService.GetPayrollDetailsAsync(id);

            return Ok(payrollDetails);
        }

        [HttpGet("employee/{employeeId:int}")]
        public async Task<IActionResult> GetPayrollByEmployeeIdAsync(int employeeId) {

            IEnumerable<PayrollDto> payrolls = await _payrollService.GetPayrollByEmployeeIdAsync(employeeId);

            return Ok(payrolls);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayrollAsync([FromBody] CreatePayrollDto createPayrollDto) {

            PayrollDto payroll = await _payrollService.CreatePayrollAsync(createPayrollDto);

            return CreatedAtAction(nameof(GetPayrollByIdAsync), new { id = payroll.PayrollId }, payroll);
        }

        [HttpPut("{id:int}/status")]
        public async Task<IActionResult> UpdatePayrollAsync(int id, [FromBody] UpdatePayrollStatusDto updatePayrollStatusDto) {

            PayrollDetailsDto updatedPayroll = await _payrollService.UpdatePayrollStatusAsync(id, updatePayrollStatusDto);

            return Ok(updatedPayroll);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePayrollAsync(int id) {

            await _payrollService.DeletePayrollAsync(id);

            return NoContent();
        }
    }
}
