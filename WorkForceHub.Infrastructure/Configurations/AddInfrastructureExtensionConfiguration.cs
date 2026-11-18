using Microsoft.Extensions.DependencyInjection;
using WorkForceHub.Application.Interfaces.RepositoryInterfaces;
using WorkForceHub.Infrastructure.Repositories;

namespace WorkForceHub.Infrastructure.Configurations
{
    public static class AddInfrastructureExtensionConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            //Add Repositories
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            services.AddScoped<IShiftRepository, ShiftRepository>();
            services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
            services.AddScoped<IPayrollRepository, PayrollRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();

            return services;
        }
    }
}

