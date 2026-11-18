using WorkForceHub.Domain.Enums;

namespace WorkForceHub.Application.DTOs.Notifications
{
    public class CreateNotificationDto
    {
        public int EmployeeId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public NotificationType Type { get; set; }
    }
}
