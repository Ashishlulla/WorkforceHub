using WorkForceHub.Domain.Entities;

namespace WorkForceHub.Application.Interfaces.RepositoryInterfaces
{
    public interface IDepartmentRepository
    {
        Task<Department> AddDepartmentAsync(Department department);
        
        Task<IEnumerable<Department>> GetDepartmentsAsync();

        Task<Department?> GetDepartmentByIdAsync(int id);
        
        Task<Department> UpdateDepartmentAsync(Department department);
        
        Task DeleteDepartmentAsync(int id);
    }
}
