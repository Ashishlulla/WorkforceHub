using WorkForceHub.Application.DTOs.LeaveRequest;

namespace WorkForceHub.Application.Interfaces.ServiceInterfaces
{
    public interface ILeaveRequestService
    {
        
        Task<LeaveRequestDto> CreateLeaveRequestAsync(CreateLeaveRequestDto createLeaveRequestDto);
        
        Task<IEnumerable<LeaveRequestDto>> GetLeaveRequestsAsync();
        
        Task<LeaveRequestDto?> GetLeaveRequestByIdAsync(int LeaveRequestId);
        
        Task<LeaveRequestDetailsDto?> GetLeaveRequestDetailsByIdAsync(int leaveRequestId);

        Task<IEnumerable<LeaveRequestDto>> GetLeaveRequestsByEmployeeIdAsync(int employeeId);
        Task<LeaveRequestDetailsDto> UpdateLeaveRequestStatusAsync(int leaveRequestId, UpdateLeaveRequestStatusDto updateLeaveRequestStatusDto);

        Task DeleteLeaveRequestAsync(int leaveRequestId);

    }
}
