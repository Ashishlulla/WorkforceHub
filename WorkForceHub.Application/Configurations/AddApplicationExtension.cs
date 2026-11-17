using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WorkForceHub.Application.Interfaces.ServiceInterfaces;
using WorkForceHub.Application.MappingProfile;
using WorkForceHub.Application.Services;
using WorkForceHub.Application.Validators.Attendance;
using WorkForceHub.Application.Validators.Department;
using WorkForceHub.Application.Validators.Employee;
using WorkForceHub.Application.Validators.LeaveRequest;
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
            });

            //Validators

            //Employee Validators
            services.AddValidatorsFromAssemblyContaining<CreateEmployeeValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateEmployeeValidator>();

            //Department Validators
            services.AddValidatorsFromAssemblyContaining<CreateDepartmentValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateDepartmentValidator>();

            //Attendance Validators
            services.AddValidatorsFromAssemblyContaining<CreateAttendanceDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateAttendanceDtoValidator>();

            //Shift Validators
            services.AddValidatorsFromAssemblyContaining<CreateShiftDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateShiftDtoValidator>();

            //LeaveRequestValidators
            services.AddValidatorsFromAssemblyContaining<CreateLeaveRequestDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateLeaveRequestStatusDtoValidator>();


            //Add Bussiness Interfaes and Services
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<IShiftService, ShiftService>();
            services.AddScoped<ILeaveRequestService, LeaveRequestService>();

            return services;
        }
    }
}