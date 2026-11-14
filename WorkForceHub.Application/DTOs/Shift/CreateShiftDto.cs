using WorkForceHub.Domain.Enums;

namespace WorkForceHub.Application.DTOs.Shift
{
    public class CreateShiftDto
    {
        public ShiftType ShiftType { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public int GracePeriodInMinutes { get; set; }

        public bool IsOvernight { get; set; } = false;

        public bool IsActive { get; set; } = true;
    }
}
