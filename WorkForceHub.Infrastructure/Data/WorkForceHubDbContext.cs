using Microsoft.EntityFrameworkCore;
using WorkForceHub.Domain.Entities;

namespace WorkForceHub.Infrastructure.Data
{
    public class WorkForceHubDbContext : DbContext
    {
        public WorkForceHubDbContext(DbContextOptions<WorkForceHubDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        
        public DbSet<Department> Departments { get; set; }
        
        public DbSet<Attendance> Attendances { get; set; }
        
        public DbSet<Shift> Shifts { get; set; }
        
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        
        public DbSet<Payroll> Payrolls { get; set; }
        
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkForceHubDbContext).Assembly);
        }

    }
}
