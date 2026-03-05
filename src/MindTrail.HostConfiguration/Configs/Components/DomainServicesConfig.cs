using Microsoft.Extensions.DependencyInjection;
using MindTrail.Domain.Abstractions.Repositories;
using MindTrail.Domain.Interfaces.Services;
using MindTrail.Domain.Services;
using MindTrail.HostConfiguration.Abstractions.Adapters.Repositories;

namespace MindTrail.HostConfiguration.Configs.Components;

/// <summary>
/// Used to configure the component <see cref="MindTrail.Domain"/>.
/// </summary>
public static class DomainServicesConfig
{
    /// <summary>
    /// Adds a configuration for domain services.
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    public static void AddDomainServicesConfig(
        this IServiceCollection services)
    {
        services.AddServices();
        services.AddRepositories();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<IPersonService, PersonService>();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services
            .AddTransient<ICountryRepository, CountryRepositoryAdapter>()
            .AddTransient<IPersonRepository, PersonRepositoryAdapter>();
    }
}