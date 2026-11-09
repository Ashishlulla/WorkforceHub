using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkForceHub.Application.Interfaces.RepositoryInterfaces;
using WorkForceHub.Infrastructure.Data;
using WorkForceHub.Infrastructure.Repositories;

namespace WorkForceHub.Infrastructure.Configurations
{
    public static class AddInfrastructureExtensionConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //Add Repositories
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();



            //Add Database

            services.AddDbContext<WorkForceHubDbContext>(options => 
            {
                options.UseSqlServer(configuration.GetConnectionString());
            });





            return services;
        }
    }
}
