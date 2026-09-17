using Microsoft.EntityFrameworkCore;
using WorkForceHub.Domain.Entities;

namespace WorkForceHub.Infrastructure.Data.Configurations
{
    public class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<LeaveRequest> builder)
        {
            builder.HasKey(lr=>lr.LeaveRequestId);

            builder.HasOne(lr => lr.Employee)
                .WithMany()
                .HasForeignKey(lr => lr.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Employee>() 
                .WithMany()
                .HasForeignKey(lr => lr.ApprovedByEmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
