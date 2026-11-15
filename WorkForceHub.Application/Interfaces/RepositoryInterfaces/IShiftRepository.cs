using WorkForceHub.Domain.Entities;

namespace WorkForceHub.Application.Interfaces.RepositoryInterfaces
{
    public interface IShiftRepository
    {
        Task<Shift> AddShiftAsync(Shift shift);

        Task<IEnumerable<Shift>> GetShiftsAsync();

        Task<Shift?> GetShiftByIdAsync(int id);
        
        Task<Shift?> GetShiftDetailsByIdAsync(int id);

        Task<Shift> UpdateShiftAsync(Shift shift);

        Task DeleteShiftAsync(Shift shift);
    }
}
