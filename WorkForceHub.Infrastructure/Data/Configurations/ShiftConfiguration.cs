using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkForceHub.Domain.Entities;

namespace WorkForceHub.Infrastructure.Data.Configurations
{
    public class ShiftConfiguration : IEntityTypeConfiguration<Shift>
    {
        public void Configure(EntityTypeBuilder<Shift> builder)
        {
            builder.HasKey(s => s.ShiftId);

            builder.Property(s => s.ShiftType)
                .IsRequired();

            builder.Property(s => s.StartTime)
                .IsRequired();

            builder.Property(s => s.EndTime)
                .IsRequired();

            builder.Property(s => s.GracePeriodInMinutes)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(s => s.IsOvernight)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(s => s.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(s => s.CreatedAt)
                .IsRequired();

            builder.Property(s => s.UpdatedAt)
                .IsRequired(false);
        }
    }
}
