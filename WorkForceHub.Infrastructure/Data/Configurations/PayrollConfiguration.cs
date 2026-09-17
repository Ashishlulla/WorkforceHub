
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkForceHub.Domain.Entities;

namespace WorkForceHub.Infrastructure.Data.Configurations
{
    public class PayrollConfiguration : IEntityTypeConfiguration<Payroll>
    {
        public void Configure(EntityTypeBuilder<Payroll> builder)
        {

            builder.Property(p => p.BasicSalary)
                .HasPrecision(18, 2);

            builder.Property(p => p.Allowances)
                .HasPrecision(18, 2);

            builder.Property(p => p.Deductions)
                .HasPrecision(18, 2);

            builder.Property(p => p.GrossSalary)
                .HasPrecision(18, 2);

            builder.Property(p => p.NetSalary)
                .HasPrecision(18, 2);

            builder.HasKey(p => p.PayrollId);

            builder.HasOne(p => p.Employee)
                .WithMany()
                .HasForeignKey(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
