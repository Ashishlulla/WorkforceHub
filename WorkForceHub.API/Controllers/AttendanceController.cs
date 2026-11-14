using Microsoft.AspNetCore.Mvc;
using WorkForceHub.Application.DTOs.Attendance;
using WorkForceHub.Application.Interfaces.ServiceInterfaces;

namespace WorkForceHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        //Private Fields
        private readonly IAttendanceService _attendanceService;

        //Constructor
        public AttendanceController(IAttendanceService attendanceService) {

            _attendanceService = attendanceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAttendancesAsync() {
            
            IEnumerable<AttendanceDto> attendances = await _attendanceService.GetAttendancesAsync();

            return Ok(attendances);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAttendanceByIdAsync(int id) {

            var attendance = await _attendanceService.GetAttendanceByIdAsync(id);

            return Ok(attendance);
        }

        [HttpGet("details/{id:int}")]
        public async Task<IActionResult> GetAttendanceDetailsByIdAsync(int id) {

            var attendanceDetails = await _attendanceService.GetAttendanceDetailsByIdAsync(id);

            return Ok(attendanceDetails);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAttendanceAsync([FromBody]CreateAttendanceDto createAttendanceDto) {

            var newAttendance = await _attendanceService.CreateAttendanceAsync(createAttendanceDto);

            return StatusCode(201, newAttendance);
        }

        [HttpPut("edit/{id:int}")]
        public async Task<IActionResult> UpdateAttendanceAsync(int id, [FromBody]UpdateAttendanceDto updateAttendanceDto)
        {
            var updatedAttendance = await _attendanceService.UpdateAttendanceAsync(id, updateAttendanceDto);

            return Ok(updatedAttendance);
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteAttendanceAsync(int id) {

            await _attendanceService.DeleteAttendanceByIdAsync(id);

            return NoContent();
        }
    }
}
