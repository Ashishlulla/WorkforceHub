using Microsoft.Extensions.DependencyInjection;
using WorkForceHub.Application.DTOs.Employee;
using WorkForceHub.Application.Interfaces.ServiceInterfaces;
using WorkForceHub.Application.MappingProfile;
using WorkForceHub.Application.Services;
using FluentValidation;               // add this or FluentValidation.AspNetCore

namespace WorkForceHub.Application.Configurations
{
    public static class AddApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //Add Services
            services.AddScoped<IEmployeeService, EmployeeService>();

            //Add AutoMapper
            services.AddAutoMapper(cfg => cfg.AddProfile<EmployeeMappingProfile>());

            //Add Validators
            services.AddValidatorsFromAssemblyContaining<CreateEmployeeDto>();

            return services;
        }
    }
}
