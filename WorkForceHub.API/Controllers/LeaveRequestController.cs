using Microsoft.AspNetCore.Mvc;
using WorkForceHub.Application.DTOs.LeaveRequest;
using WorkForceHub.Application.Interfaces.ServiceInterfaces;

namespace WorkForceHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        // Private Fields
        private readonly ILeaveRequestService _leaveRequestService;

        //Constructor
        public LeaveRequestController(ILeaveRequestService leaveRequestService) {

            _leaveRequestService = leaveRequestService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLeaveRequestsAsync() {

            IEnumerable<LeaveRequestDto> leaveRequests = await _leaveRequestService.GetLeaveRequestsAsync();

            return Ok(leaveRequests);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetLeaveRequestByIdAsync(int id) {

            LeaveRequestDto? leaveRequest = await _leaveRequestService.GetLeaveRequestByIdAsync(id);

            return Ok(leaveRequest);
        }

        [HttpGet("employeeId/{employeeId:int}")]
        public async Task<IActionResult> GetLeaveRequestByEmployeeIdAsync(int employeeId)
        {
            IEnumerable<LeaveRequestDto> leaveRequests = await _leaveRequestService.GetLeaveRequestsByEmployeeIdAsync(employeeId);

            return Ok(leaveRequests);
        }

        [HttpGet("details/{id:int}")]
        public async Task<IActionResult> GetLeaveRequestDetailsByIdAsync(int id) {

            LeaveRequestDetailsDto? leaveRequestDetails = await _leaveRequestService.GetLeaveRequestDetailsByIdAsync(id);

            return Ok(leaveRequestDetails);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLeaveRequestAsync([FromBody] CreateLeaveRequestDto createLeaveRequestDto) {

            LeaveRequestDto addedLeaveRequest = await _leaveRequestService.CreateLeaveRequestAsync(createLeaveRequestDto);

            return StatusCode(201, addedLeaveRequest);
        }

        [HttpPut("{id:int}/status")]
        public async Task<IActionResult> UpdateLeaveRequestAsync(int id, [FromBody] UpdateLeaveRequestStatusDto updateLeaveRequestStatusDto) {

            LeaveRequestDetailsDto updatedLeaveRequest = await _leaveRequestService.UpdateLeaveRequestStatusAsync(id, updateLeaveRequestStatusDto);

            return Ok(updatedLeaveRequest);


         }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLeaveRequestAsync(int id) {

            await _leaveRequestService.DeleteLeaveRequestAsync(id);

            return NoContent();
        }

    }
}
