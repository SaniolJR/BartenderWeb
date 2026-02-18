using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
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
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MainDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IUserSeeder, UserSeeder>();
            services.AddScoped<IDrinkRepository, DrinkRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        }
    }
}