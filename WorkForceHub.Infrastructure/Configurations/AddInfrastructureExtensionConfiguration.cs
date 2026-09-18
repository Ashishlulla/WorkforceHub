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





            return services;
        }
    }
}
