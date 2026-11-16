using WorkForceHub.Domain.Entities;

namespace WorkForceHub.Application.Interfaces.RepositoryInterfaces
{
    public interface ILeaveRequestRepository
    {
        Task<LeaveRequest> AddLeaveRequestAsync( LeaveRequest leaveRequest);

        Task<IEnumerable<LeaveRequest>> GetLeaveRequestsAsync();

        Task<LeaveRequest?> GetLeaveRequestByIdAsync(int leaveRequestId);

        Task<LeaveRequest?> GetLeaveRequestDetailsByIdAsync(int leaveRequestId);

        Task<IEnumerable<LeaveRequest>> GetLeaveRequestByEmployeeIdAsync(int EmployeeId);

        Task<LeaveRequest> UpdateLeaveRequestAsync(LeaveRequest leaveRequest);

        Task DeleteLeaveRequestByIdAsync(LeaveRequest leaveRequest);

    }
}
