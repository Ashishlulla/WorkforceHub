using WorkForceHub.Application.DTOs.Notifications;

namespace WorkForceHub.Application.Interfaces.ServiceInterfaces
{
    public interface INotificationService
    {
        
        Task<NotificationDto> CreateNotificationAsync(CreateNotificationDto createNotificationDto);

        Task<IEnumerable<NotificationDto>> GetNotificationsAsync();

        Task<NotificationDto> GetNotificationByIdAsync(int id);

        Task<IEnumerable<NotificationDto>> GetNotificationsByEmployeeIdAsync(int employeeId);
        
        Task<IEnumerable<NotificationDto>> GetUnreadNotificationsAsync();

        Task<IEnumerable<NotificationDto>> GetUnreadNotificationsByAdminAsync();

        Task<IEnumerable<NotificationDto>> GetUnreadNotificationsByEmployeeIdAsync(int employeeId);

        Task MarkNotificationAsReadAsync(int id);

        Task MarkAllAsReadAsync(int employeeId);

        Task MarkAllAsReadByAdminAsync();

        Task DeleteNotificationAsync(int id);
    }
}
