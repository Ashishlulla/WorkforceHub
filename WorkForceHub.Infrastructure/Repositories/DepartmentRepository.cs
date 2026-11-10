
using Microsoft.EntityFrameworkCore;
using WorkForceHub.Application.Interfaces.RepositoryInterfaces;
using WorkForceHub.Domain.Entities;
using WorkForceHub.Infrastructure.Data;

namespace WorkForceHub.Infrastructure.Repositories
{
    public class DepartmentRepository: IDepartmentRepository
    {
        //Private Fields
        private readonly WorkForceHubDbContext _db;

        //Constructor
        public DepartmentRepository(WorkForceHubDbContext db) {

            _db = db;
        }

        public async Task<Department> AddDepartmentAsync(Department department)
        {
            await _db.Departments.AddAsync(department);
            await _db.SaveChangesAsync();
            return department;
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var department = await _db.Departments.FindAsync(id);

            _db.Departments.Remove(department);
            await _db.SaveChangesAsync();
        }

        public async Task<Department?> GetDepartmentByIdAsync(int id)
        {
            var department = await _db.Departments.FirstOrDefaultAsync(d => d.DepartmentId == id);
            return department;
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            var departments = await _db.Departments.ToListAsync();
            return departments;
        }

        public async Task<Department> UpdateDepartmentAsync(Department department)
        {
            _db.Departments.Update(department);
            await _db.SaveChangesAsync();
            return department;
        }
    }
}
