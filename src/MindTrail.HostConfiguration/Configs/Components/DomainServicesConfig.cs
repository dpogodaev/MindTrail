using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MindTrail.DomainServices.Interfaces.Services;
using MindTrail.DomainServices.Interfaces.Storages.Repositories;
using MindTrail.DomainServices.Services;
using MindTrail.EfCore.Adapters.Repositories;
using MindTrail.HostConfiguration.Interfaces;

namespace MindTrail.HostConfiguration.Configs.Components;

/// <summary>
/// Used to configure the component <see cref="MindTrail.DomainServices"/>.
/// </summary>
public static class DomainServicesConfig
{
    /// <summary>
    /// Adds a configuration for domain services.
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="logger">The startup logger. Optional.</param>
    public static void AddDomainServicesConfig(
        this IServiceCollection services, IConfiguration configuration, IStartupLogger logger = null)
    {
        AddServices(services);
        AddRepositories(services);
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddTransient<IPersonService, PersonService>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddTransient<ICountryRepository, CountryRepositoryAdapter>();
        services.AddTransient<IPersonRepository, PersonRepositoryAdapter>();
    }
}