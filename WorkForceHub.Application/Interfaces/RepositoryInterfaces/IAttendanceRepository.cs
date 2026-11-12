using WorkForceHub.Domain.Entities;

namespace WorkForceHub.Application.Interfaces.RepositoryInterfaces
{
    public interface IAttendanceRepository
    {
        Task<Attendance> AddAttendanceAsync(Attendance attendance);
        
        Task<IEnumerable<Attendance>> GetAttendancesAsync();
        
        Task<Attendance?> GetAttendanceByIdAsync(int id);
        
        Task<Attendance?> GetAttendanceDetailsByIdAsync(int id);
        
        Task<Attendance> UpdateAttendanceAsync(Attendance attendance);
        
        Task DeleteAttendanceByIdAsync(Attendance attendance);
    }
}
