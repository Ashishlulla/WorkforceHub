using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WorkForceHub.Application.Interfaces.ServiceInterfaces;
using WorkForceHub.Application.MappingProfile;
using WorkForceHub.Application.Services;
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
            });

            //Validators

            //Employee Validators
            services.AddValidatorsFromAssemblyContaining<CreateEmployeeValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateEmployeeValidator>();

            //Department Validators
            services.AddValidatorsFromAssemblyContaining<CreateDepartmentValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateDepartmentValidator>();

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();

            return services;
        }
    }
}