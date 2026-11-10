using WorkForceHub.Application.DTOs.Department;

namespace WorkForceHub.Application.Interfaces.ServiceInterfaces
{
    public interface IDepartmentService
    {
        Task<DepartmentDto> AddDepartmentAsync(CreateDepartmentDto createDepartmentDto);
        
        Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync();

        Task<DepartmentDto?> GetDepartmentByIdAsync(int id);

        Task<DepartmentDto> UpdateDepartmentAsync(int id, UpdateDepartmentDto updateDepartmentDto);

        Task DeleteDepartmentByIdAsync(int id);
    }
}
