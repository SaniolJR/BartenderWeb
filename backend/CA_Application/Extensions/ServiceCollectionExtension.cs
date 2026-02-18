using Microsoft.Extensions.DependencyInjection;
using CA_Application;

namespace CA_Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDrinkService, DrinkService>();
        services.AddScoped<IIngredientService, IngredientService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IUserService, UserService>();
    }
}