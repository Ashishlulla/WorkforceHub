using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WorkForceHub.Application.Interfaces.ServiceInterfaces;
using WorkForceHub.Application.MappingProfile;
using WorkForceHub.Application.Services;
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
            });

            services.AddValidatorsFromAssemblyContaining<CreateEmployeeValidator>();

            services.AddScoped<IEmployeeService, EmployeeService>();

            return services;
        }
    }
}