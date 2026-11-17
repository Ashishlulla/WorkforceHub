using Microsoft.EntityFrameworkCore;
using WorkForceHub.Application.Interfaces.RepositoryInterfaces;
using WorkForceHub.Domain.Entities;
using WorkForceHub.Infrastructure.Data;

namespace WorkForceHub.Infrastructure.Repositories
{
    public class PayrollRepository : IPayrollRepository
    {
        // Private Fields
        private readonly WorkForceHubDbContext _db;

        //Constructor
        public PayrollRepository(WorkForceHubDbContext db) {

            _db = db;
        }

        public async Task<Payroll> AddPayrollAsync(Payroll payroll)
        {
            await _db.Payrolls.AddAsync(payroll);
            await _db.SaveChangesAsync();

            return payroll;
        }

        public async Task DeletePayrollAsync(Payroll payroll)
        {
            _db.Payrolls.Remove(payroll);
            await _db.SaveChangesAsync();

        }

        public async Task<Payroll?> GetPayrollByIdAsync(int id)
        {
            return await _db.Payrolls.Include(p => p.Employee).FirstOrDefaultAsync(p => p.PayrollId == id);
        }

        public async Task<IEnumerable<Payroll>> GetPayrollsAsync()
        {
            return await _db.Payrolls.AsNoTracking().Include(p => p.Employee).ToListAsync();
        }

        public async Task<IEnumerable<Payroll>> GetPayrollsByEmployeeIdAsync(int employeeId)
        {
            return await _db.Payrolls.AsNoTracking().Include(p => p.Employee).Where(p => p.EmployeeId == employeeId).ToListAsync();
        }

        public async Task<bool> PayrollExistsAsync(int employeeId, int payrollYear, int payRollMonth)
        {
            return await _db.Payrolls.AnyAsync(p=>
            p.EmployeeId == employeeId &&
            p.PayrollYear == payrollYear &&
            p.PayrollMonth == payRollMonth);
        }

        public async Task<Payroll> UpdatePayrollAsync(Payroll payroll)
        {
            _db.Payrolls.Update(payroll);
            await _db.SaveChangesAsync();

            return payroll;
        }
    }
}
