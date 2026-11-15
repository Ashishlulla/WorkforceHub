using Microsoft.AspNetCore.Mvc;
using WorkForceHub.Application.DTOs.Shift;
using WorkForceHub.Application.Interfaces.ServiceInterfaces;

namespace WorkForceHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        //Private Fields
        private readonly IShiftService _shiftService;

        //Constructor
        public ShiftController(IShiftService shiftService) {

            _shiftService = shiftService;
        }

        [HttpGet]
        public async Task<IActionResult> GetShiftsAsync() 
        {
            var shifts = await _shiftService.GetShiftsAsync();

            return Ok(shifts);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetShiftByIdAsync(int id) {

            var shift = await _shiftService.GetShiftByIdAsync(id);

            return Ok(shift);
        }

        [HttpGet("details/{id:int}")]
        public async Task<IActionResult> GetShiftDetailsByIdAsync(int id) {

            var shiftDetails = await _shiftService.GetShiftDetailsByIdAsync(id);

            return Ok(shiftDetails);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShiftAsync([FromBody] CreateShiftDto createShiftDto) {

            var createdShift = await _shiftService.CreateShiftAsync(createShiftDto);

            return StatusCode(201, createdShift);
        }

        [HttpPut("edit/{id:int}")]
        public async Task<IActionResult> UpdateShiftAsync(int id, [FromBody] UpdateShiftDto updateShiftDto) {

            var updatedShift = await _shiftService.UpdateShiftAsync(id, updateShiftDto);

            return Ok(updatedShift);
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteShiftByIdAsync(int id) {

            await _shiftService.DeleteShiftAsync(id);

            return NoContent();
        }
    }
}
