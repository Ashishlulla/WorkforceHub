using WorkForceHub.Domain.Enums;

namespace WorkForceHub.Domain.Entities
{
    public class Notification
    {
        public int NotificationId { get; set; }

        public int EmployeeId { get; set; }
        
        public string Title { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public NotificationType Type { get; set; }

        public bool IsRead { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public DateTime? ReadAt { get; set; }

        public bool IsReadByAdmin { get; set; } = false;
        
        public DateTime? AdminReadAt { get; set; }

        // Navigation Property

        public Employee Employee { get; set; } = null!;
    }
}
