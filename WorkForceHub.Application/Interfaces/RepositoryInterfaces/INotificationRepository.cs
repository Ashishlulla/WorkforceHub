using WorkForceHub.Domain.Entities;

namespace WorkForceHub.Application.Interfaces.RepositoryInterfaces
{
    public interface INotificationRepository
    {
        Task<Notification> AddNotificationAsync(Notification notification);

        Task<IEnumerable<Notification>> GetNotificationsAsync();

        Task<Notification?> GetNotificationByIdAsync(int id);

        Task<IEnumerable<Notification>> GetNotificationsByEmployeeIdAsync(int employeeId);

        Task<IEnumerable<Notification>> GetUnReadNotificationAsync();
        Task<IEnumerable<Notification>> GetUnreadNotificationsByAdminAsync();

        Task<IEnumerable<Notification>> GetUnReadNotificationByEmployeeIdAsync(int employeeId);

        Task UpdateNotificationAsync(Notification notification);

        Task MarkAllAsReadAsync(int employeeId);

        Task MarkAllAsReadByAdminAsync();

        Task DeleteNotificationAsync(Notification notification);
    }
}
