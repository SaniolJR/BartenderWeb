using Microsoft.Extensions.DependencyInjection;
using CA_Application;

namespace CA_Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDrinkService, DrinkService>();
    }
}