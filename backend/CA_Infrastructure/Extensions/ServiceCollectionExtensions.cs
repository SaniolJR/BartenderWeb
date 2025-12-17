using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CA_Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Add DB using Dependency Injection
            var connectionString = configuration.GetConnectionString("DefaultConnection"); // poprawiona nazwa zmiennej
            services.AddDbContext<MainDbContext>(options => options.UseSqlServer(connectionString)); // poprawiona nazwa parametru i zmiennej
        }
    }
}