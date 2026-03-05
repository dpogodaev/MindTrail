using Microsoft.Extensions.DependencyInjection;
using MindTrail.Application.Abstractions.Repositories;
using MindTrail.Application.Services;
using MindTrail.ApplicationContracts.Interfaces.Services;
using MindTrail.HostConfiguration.Abstractions.Adapters.Repositories;
using MindTrail.HostConfiguration.Logging.Services;

namespace MindTrail.HostConfiguration.Configs.Components;

/// <summary>
/// Used to configure the component <see cref="MindTrail.AppServices"/>.
/// </summary>
public static class AppServicesConfig
{
    /// <summary>
    /// Adds a configuration for application services.
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    public static void AddAppServicesConfig(
        this IServiceCollection services)
    {
        services.AddAdapters();
        services.AddAppServices();
    }

    private static void AddAdapters(this IServiceCollection services)
    {
        services
            .AddScoped<IUnitOfWork, UnitOfWorkAdapter>()
            .AddScoped<ICountryReadRepository, CountryReadRepositoryAdapter>();
    }

    private static void AddAppServices(this IServiceCollection services)
    {
        services
            .AddScoped<ICountryAppService, CountryAppService>()
            .AddScoped<IPersonAppService, PersonAppService>()
            .Decorate<IPersonAppService, PersonAppServiceLogging>();
    }
}