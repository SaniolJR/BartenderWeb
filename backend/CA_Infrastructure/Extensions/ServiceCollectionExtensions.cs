using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization;
using CA_Infrastructure.DataSeeders;
using CA_Infrastructure.Database;
using CA_Domain.Repositories;
using CA_Infrastructure.Repositories;

namespace CA_Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Add DB using Dependency Injection
            var connectionString = configuration.GetConnectionString("DefaultConnection"); // poprawiona nazwa zmiennej
            services.AddDbContext<MainDbContext>(options => options.UseSqlServer(connectionString)); // poprawiona nazwa parametru i zmiennej

            services.AddScoped<IUserSeeder, UserSeeder>();
            services.AddScoped<IDrinkRepository, DrinkRepository>();

        }
    }
}