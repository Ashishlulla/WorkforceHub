using Microsoft.EntityFrameworkCore;
using WorkForceHub.Application.Interfaces.RepositoryInterfaces;
using WorkForceHub.Domain.Entities;
using WorkForceHub.Infrastructure.Data;

namespace WorkForceHub.Infrastructure.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        //Private Fields
        private readonly WorkForceHubDbContext _db;

        //Constructor
        public AttendanceRepository(WorkForceHubDbContext db) {

            _db = db;
        }

        public async Task<Attendance> AddAttendanceAsync(Attendance attendance)
        {
            await _db.Attendances.AddAsync(attendance);
            await _db.SaveChangesAsync();

            return attendance;
        }

        public async Task DeleteAttendanceByIdAsync( Attendance attendance)
        {
            
            _db.Attendances.Remove(attendance);
            await _db.SaveChangesAsync();
        }

        public async Task<Attendance?> GetAttendanceByEmployeeIdAndDateAsync(int employeeId, DateOnly date)
        {
            return await _db.Attendances.FirstOrDefaultAsync(a => a.EmployeeId == employeeId && a.AttendanceDate == date);
        }

        public async Task<Attendance?> GetAttendanceByIdAsync(int id)
        {
            return await _db.Attendances.FindAsync(id);
            
        }

        public async Task<Attendance?> GetAttendanceDetailsByIdAsync(int id)
        {
            return await _db.Attendances.Include(a=>a.Employee).FirstOrDefaultAsync(a=>a.AttendanceId == id);
            
        }

        public async Task<IEnumerable<Attendance>> GetAttendancesAsync()
        {
            return await _db.Attendances.AsNoTracking().ToListAsync();
        }

        public async Task<Attendance> UpdateAttendanceAsync(Attendance attendance)
        {
            _db.Attendances.Update(attendance);
            await _db.SaveChangesAsync();

            return attendance;
        }
    }
}
