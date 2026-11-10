
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkForceHub.Domain.Entities;

namespace WorkForceHub.Infrastructure.Data.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            //Primary Key
            builder.HasKey(d => d.DepartmentId);

            //Deprtment Name
            builder.Property(d => d.DepartmentName)
                .IsRequired()
                .HasMaxLength(100);

            //Departmemt Description
            builder.Property(d => d.Description)
                .HasMaxLength(500);

            //Department IsActive
            builder.Property(d => d.IsActive)
                .HasDefaultValue(true);

            //Created At
            builder.Property(d => d.CreatedAt)
                .IsRequired();

            //Updated At
            builder.Property(d => d.UpdatedAt)
                .IsRequired(false);
        }
    }
}
