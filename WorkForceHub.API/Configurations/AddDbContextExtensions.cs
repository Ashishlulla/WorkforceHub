using Microsoft.EntityFrameworkCore;
using WorkForceHub.Infrastructure.Data;

namespace WorkForceHub.API.Configurations
{
    public static class AddDbContextExtensions
    {
        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<WorkForceHubDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}