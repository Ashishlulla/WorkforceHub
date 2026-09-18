using Microsoft.EntityFrameworkCore;
using WorkForceHub.Application.Interfaces.RepositoryInterfaces;
using WorkForceHub.Domain.Entities;
using WorkForceHub.Infrastructure.Data;

namespace WorkForceHub.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        //Private Feilds
        private readonly WorkForceHubDbContext _db;

        //Constructor
        public EmployeeRepository(WorkForceHubDbContext db) 
        {
            _db = db;
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
           await _db.Employees.AddAsync(employee);
           await _db.SaveChangesAsync();
        }

        public async Task DeleteEmployeeByIdAsync(int id)
        {
            var employee = await _db.Employees.FindAsync(id);
            
            _db.Employees.Remove(employee!);
            await _db.SaveChangesAsync();
            
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            
            return await _db.Employees.ToListAsync();
        
        }

        public async Task<Employee?> GetEmployeeByCodeAsync(string code)
        {
            return await _db.Employees.FirstOrDefaultAsync(e => e.EmployeeCode == code);
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _db.Employees.FindAsync(id);
        
        }

        public Task UpdateEmployeeAsync(Employee employee)
        {
           _db.Employees.Update(employee);
           return _db.SaveChangesAsync();
        }
    }
}
