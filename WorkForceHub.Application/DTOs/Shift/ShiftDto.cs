using WorkForceHub.Domain.Enums;

namespace WorkForceHub.Application.DTOs.Shift
{
    public class ShiftDto
    {
        public int ShiftId { get; set; }

        public ShiftType ShiftType { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public int GracePeriodInMinutes { get; set; }

        public bool IsOvernight { get; set; }

        public bool IsActive { get; set; } 

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
