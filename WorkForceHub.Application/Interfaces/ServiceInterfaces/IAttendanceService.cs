using WorkForceHub.Application.DTOs.Attendance;

namespace WorkForceHub.Application.Interfaces.ServiceInterfaces
{
    public interface IAttendanceService
    {
        Task<AttendanceDto> CreateAttendanceAsync(CreateAttendanceDto createAttendanceDto);

        Task<IEnumerable<AttendanceDto>> GetAttendancesAsync();

        Task<AttendanceDto> GetAttendanceByIdAsync(int id);

        Task<AttendanceDetailsDto> GetAttendanceDetailsByIdAsync(int id);

        Task<AttendanceDto> UpdateAttendanceAsync(int id, UpdateAttendanceDto updateAttendanceDto);

        Task DeleteAttendanceByIdAsync(int id);
    }
}
