using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WorkForceHub.Application.DTOs.Attendance;
using WorkForceHub.Application.Interfaces.ServiceInterfaces;
using WorkForceHub.Application.MappingProfile;
using WorkForceHub.Application.Services;
using WorkForceHub.Application.Validators.Attendance;
using WorkForceHub.Application.Validators.Department;
using WorkForceHub.Application.Validators.Employee;

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
            });

            //Validators

            //Employee Validators
            services.AddValidatorsFromAssemblyContaining<CreateEmployeeValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateEmployeeValidator>();

            //Department Validators
            services.AddValidatorsFromAssemblyContaining<CreateDepartmentValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateDepartmentValidator>();

            //AttendanceValidators
            services.AddValidatorsFromAssemblyContaining<CreateAttendanceDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateDepartmentValidator>();

            //Add Bussiness Interfaes and Services
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IAttendanceService, AttendanceService>();

            return services;
        }
    }
}