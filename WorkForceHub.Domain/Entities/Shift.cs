
using WorkForceHub.Domain.Enums;

namespace WorkForceHub.Domain.Entities
{
    public class Shift
    {
        public int ShiftId { get; set; }
        
        public ShiftType ShiftType { get; set; } = ShiftType.General;
        
        public TimeSpan StartTime { get; set; }
        
        public TimeSpan EndTime { get; set; }
        
        public int GracePeriodInMinutes { get; set; }
        
        public bool IsOvernight { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }

    }
}
