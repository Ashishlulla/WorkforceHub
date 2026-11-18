using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WorkForceHub.Application.Interfaces.ServiceInterfaces;
using WorkForceHub.Application.MappingProfile;
using WorkForceHub.Application.Services;
using WorkForceHub.Application.Validators.Attendance;
using WorkForceHub.Application.Validators.Department;
using WorkForceHub.Application.Validators.Employee;
using WorkForceHub.Application.Validators.LeaveRequest;
using WorkForceHub.Application.Validators.Notifications;
using WorkForceHub.Application.Validators.Shift;

namespace WorkForceHub.Application.Configurations
{
    public static class AddApplicationExtensions
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<EmployeeMappingProfile>();
                cfg.AddProfile<DepartmentMappingProfile>();
                cfg.AddProfile<AttendanceMappingProfile>();
                cfg.AddProfile<ShiftMappingProfile>();
                cfg.AddProfile<LeaveRequestMappingProfile>();
                cfg.AddProfile<PayrollMappingProfile>();
                cfg.AddProfile<NotificationMappingProfile>();
            });

            //Dtos Validators

            //EmployeeDtos Validators
            services.AddValidatorsFromAssemblyContaining<CreateEmployeeValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateEmployeeValidator>();

            //DepartmentDtos Validators
            services.AddValidatorsFromAssemblyContaining<CreateDepartmentValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateDepartmentValidator>();

            //AttendanceDtos Validators
            services.AddValidatorsFromAssemblyContaining<CreateAttendanceDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateAttendanceDtoValidator>();

            //ShiftDtos Validators
            services.AddValidatorsFromAssemblyContaining<CreateShiftDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateShiftDtoValidator>();

            //LeaveRequestDtos Validators
            services.AddValidatorsFromAssemblyContaining<CreateLeaveRequestDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateLeaveRequestStatusDtoValidator>();

            //NotificationDtos Validators
            services.AddValidatorsFromAssemblyContaining<CreateNotificationDtoValidator>();


            //Add Bussiness Interfaes and Services
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<IShiftService, ShiftService>();
            services.AddScoped<ILeaveRequestService, LeaveRequestService>();
            services.AddScoped<IPayrollService, PayrollService>();
            services.AddScoped<INotificationService, NotificationService>();

            return services;
        }
    }
}