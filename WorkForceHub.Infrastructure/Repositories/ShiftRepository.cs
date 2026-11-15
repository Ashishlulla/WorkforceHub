using Microsoft.EntityFrameworkCore;
using WorkForceHub.Application.Interfaces.RepositoryInterfaces;
using WorkForceHub.Domain.Entities;
using WorkForceHub.Infrastructure.Data;

namespace WorkForceHub.Infrastructure.Repositories
{
    public class ShiftRepository : IShiftRepository
    {
        //Private Fields
        private readonly WorkForceHubDbContext _db;

        //Constructor
        public ShiftRepository(WorkForceHubDbContext db) {

            _db = db;
        }

        public async Task<Shift> AddShiftAsync(Shift shift)
        {
            await _db.Shifts.AddAsync(shift);
            await _db.SaveChangesAsync();

            return shift;
        }

        public async Task DeleteShiftAsync(Shift shift)
        {
            _db.Shifts.Remove(shift);
            await _db.SaveChangesAsync();
        }

        public async Task<Shift?> GetShiftByIdAsync(int id)
        {
            return await _db.Shifts.FirstOrDefaultAsync(s=>s.ShiftId == id);
        }

        public async Task<Shift?> GetShiftDetailsByIdAsync(int id)
        {
            return await _db.Shifts.Include(s => s.Employees).FirstOrDefaultAsync(s => s.ShiftId == id);
        }

        public async Task<IEnumerable<Shift>> GetShiftsAsync()
        {
            return await _db.Shifts.AsNoTracking().ToListAsync();
        }

        public async Task<Shift> UpdateShiftAsync(Shift shift)
        {
            _db.Shifts.Update(shift);
            await _db.SaveChangesAsync();

            return shift;
        }
    }
}
