using WorkForceHub.Application.DTOs.Shift;

namespace WorkForceHub.Application.Interfaces.ServiceInterfaces
{
    public interface IShiftService
    {
        
        Task<ShiftDto> CreateShiftAsync(CreateShiftDto createShiftDto);
        
        Task<IEnumerable<ShiftDto>> GetShiftsAsync();
        
        Task<ShiftDto> GetShiftByIdAsync(int id);
        
        Task<ShiftDetailsDto> GetShiftDetailsByIdAsync(int id);
        
        Task<ShiftDto> UpdateShiftAsync(int id , UpdateShiftDto updateShiftDto);
        
        Task DeleteShiftAsync(int id);
    
    }
}
