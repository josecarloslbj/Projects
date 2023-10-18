using JC.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JC.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddApplicationServices();

        return services;
    }

    private static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoriaService, CategoriaService>();

        return services;
    }
}
