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
            services.AddValidatorsFromAssemblyContaining<UpdateDepartmentValidator>();

            //Shift Validators
            services.AddValidatorsFromAssemblyContaining<CreateShiftDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateShiftDtoValidator>();

            //Add Bussiness Interfaes and Services
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IAttendanceService, AttendanceService>();

            return services;
        }
    }
}