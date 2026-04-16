using Microsoft.Extensions.DependencyInjection;
using MindTrail.Application.Abstractions.QueryServices;
using MindTrail.Application.Abstractions.Repositories;
using MindTrail.Application.AppServices;
using MindTrail.ApplicationConfigurator.Abstractions.Adapters.QueryServices;
using MindTrail.ApplicationConfigurator.Abstractions.Adapters.Repositories;
using MindTrail.ApplicationConfigurator.Logging.Services;
using MindTrail.ApplicationContracts.Interfaces.AppServices;

namespace MindTrail.ApplicationConfigurator.Configs.Components;

/// <summary>
/// Used to configure the component <see cref="MindTrail.Application"/>.
/// </summary>
public static class ApplicationConfig
{
    /// <summary>
    /// Adds a configuration for application services.
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    public static void AddApplicationConfig(
        this IServiceCollection services)
    {
        services.AddAppServices();
        services.AddQueryServices();
        services.AddRepositories();
    }

    private static void AddAppServices(this IServiceCollection services)
    {
        services
            .AddScoped<ICountryAppService, CountryAppService>()
            .AddScoped<IPersonAppService, PersonAppService>()
            .Decorate<IPersonAppService, PersonAppServiceLogging>();
    }

    private static void AddQueryServices(this IServiceCollection services)
    {
        services
            .AddScoped<ICountryQueryService, CountryQueryServiceAdapter>()
            .AddScoped<IPersonQueryService, PersonQueryServiceAdapter>();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IUnitOfWork, UnitOfWorkAdapter>()
            .AddTransient<ICountryRepository, CountryRepositoryAdapter>()
            .AddTransient<IPersonRepository, PersonRepositoryAdapter>();
    }
}