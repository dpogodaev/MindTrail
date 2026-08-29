using Microsoft.Extensions.DependencyInjection;
using MindTrail.Application.Abstractions.Repositories;
using MindTrail.EfCore.Repositories;

namespace MindTrail.ApplicationConfigurator.Configs.Components;

/// <summary>
/// Used to configure the component <see cref="MindTrail.Application"/>.
/// </summary>
public static class ApplicationConfig
{
    /// <summary>
    /// Adds a configuration for the application implementation.
    /// </summary>
    /// <param name="services">The service collection used to register application services.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance, so that additional calls can be chained.</returns>
    public static IServiceCollection AddApplicationConfig(this IServiceCollection services)
    {
        AddRepositories(services);

        return services;
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services
            .AddScoped<ICountryRepository, CountryRepository>()
            .AddScoped<IPersonRepository, PersonRepository>()
            .AddScoped<ICardRepository, CardRepository>();
    }
}