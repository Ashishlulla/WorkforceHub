using Microsoft.EntityFrameworkCore;
using WorkForceHub.Application.Interfaces.RepositoryInterfaces;
using WorkForceHub.Domain.Entities;
using WorkForceHub.Infrastructure.Data;

namespace WorkForceHub.Infrastructure.Repositories
{
    public class LeaveRequestRepository :ILeaveRequestRepository
    {
        // Private Fields
        private readonly WorkForceHubDbContext _db;

        //Constructor
        public LeaveRequestRepository(WorkForceHubDbContext db) {

            _db = db;
        }

        public async Task<LeaveRequest> AddLeaveRequestAsync(LeaveRequest leaveRequest)
        {
            await _db.LeaveRequests.AddAsync(leaveRequest);
            await _db.SaveChangesAsync();
            return leaveRequest;
        }

        public async Task DeleteLeaveRequestByIdAsync(LeaveRequest leaveRequest)
        {
            _db.LeaveRequests.Remove(leaveRequest);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<LeaveRequest>> GetLeaveRequestByEmployeeIdAsync(int EmployeeId)
        {
            return await _db.LeaveRequests.Include(lr => lr.Employee).Where(lr => lr.EmployeeId == EmployeeId).ToListAsync();
        }

        public async Task<LeaveRequest?> GetLeaveRequestByIdAsync(int leaveRequestId)
        {
            return await _db.LeaveRequests.FirstOrDefaultAsync(lr => lr.LeaveRequestId == leaveRequestId);
        }

        public async Task<LeaveRequest?> GetLeaveRequestDetailsByIdAsync(int leaveRequestId)
        {
            return await _db.LeaveRequests.Include(lr => lr.Employee).Include(lr => lr.ApprovedByEmployee).FirstOrDefaultAsync(lr => lr.LeaveRequestId == leaveRequestId);
        }

        public async Task<IEnumerable<LeaveRequest>> GetLeaveRequestsAsync()
        {
            return await _db.LeaveRequests.AsNoTracking().Include(lr=>lr.Employee).ToListAsync();
        }

        public async Task<LeaveRequest> UpdateLeaveRequestAsync(LeaveRequest leaveRequest)
        {
            _db.LeaveRequests.Update(leaveRequest);
            await _db.SaveChangesAsync();

            return leaveRequest;
        }
    }
}
