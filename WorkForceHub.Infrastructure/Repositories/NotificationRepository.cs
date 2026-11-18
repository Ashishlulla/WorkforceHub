using Microsoft.EntityFrameworkCore;
using WorkForceHub.Application.Interfaces.RepositoryInterfaces;
using WorkForceHub.Domain.Entities;
using WorkForceHub.Infrastructure.Data;

namespace WorkForceHub.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        // Private Fields
        private readonly WorkForceHubDbContext _db;

        //Constructor
        public NotificationRepository(WorkForceHubDbContext db) {

            _db = db;
        }

        public async Task<Notification> AddNotificationAsync(Notification notification)
        {
            await _db.Notifications.AddAsync(notification);
            await _db.SaveChangesAsync();
            
            return notification;
        }

        public async Task DeleteNotificationAsync(Notification notification)
        {
            _db.Notifications.Remove(notification);
            await _db.SaveChangesAsync();
        }

        public async Task<Notification?> GetNotificationByIdAsync(int id)
        {
            return await _db.Notifications.Include(n => n.Employee).FirstOrDefaultAsync(n => n.NotificationId == id);
        }

        public async Task<IEnumerable<Notification>> GetNotificationsAsync()
        {
            return await _db.Notifications.Include(n => n.Employee).ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByEmployeeIdAsync(int employeeId)
        {
            return await _db.Notifications.Include(n => n.Employee).Where(n => n.EmployeeId == employeeId).ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetUnReadNotificationAsync()
        {
            return await _db.Notifications.Include(n=>n.Employee).Where(n => n.IsRead == false).ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetUnReadNotificationByEmployeeIdAsync(int employeeId)
        {
            return await _db.Notifications.Include(n=>n.Employee).Where(n => n.EmployeeId == employeeId && n.IsRead == false).ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetUnreadNotificationsByAdminAsync()
        {
            IEnumerable<Notification> notifications = await _db.Notifications.Include(n => n.Employee).Where(n => n.IsReadByAdmin == false).ToListAsync();

            return notifications;
        }

        public async Task MarkAllAsReadAsync(int employeeId)
        {
            IEnumerable<Notification> notifications = await _db.Notifications.Where(n=>n.EmployeeId == employeeId && !n.IsRead).ToListAsync();

            foreach (var notification in notifications) {

                notification.IsRead = true;
                notification.ReadAt = DateTime.UtcNow;
            }

            await _db.SaveChangesAsync();
        }

        public async Task MarkAllAsReadByAdminAsync()
        {
            IEnumerable<Notification> notifications = await _db.Notifications.Where(n=>n.IsReadByAdmin == false).ToListAsync();

            foreach (Notification notification in notifications) {

                notification.IsReadByAdmin = true;
                notification.AdminReadAt = DateTime.UtcNow;
            }

            await _db.SaveChangesAsync();
        }

        public async Task UpdateNotificationAsync(Notification notification)
        {
            _db.Notifications.Update(notification);
            await _db.SaveChangesAsync();
        }
    }
}
