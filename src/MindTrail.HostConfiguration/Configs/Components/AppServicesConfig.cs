using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MindTrail.AppServices.Interfaces.Services;
using MindTrail.AppServices.Services;
using MindTrail.HostConfiguration.Interfaces;

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
    /// <param name="configuration">The application configuration.</param>
    /// <param name="logger">The startup logger. Optional.</param>
    public static void AddAppServicesConfig(this IServiceCollection services,
        IConfiguration configuration, IStartupLogger logger = null)
    {
        AddAppServices(services);
    }

    #region Private methods

    private static void AddAppServices(IServiceCollection services)
    {
        services.AddScoped<ICountryAppService, CountryAppService>();
        services.AddScoped<IPersonAppService, PersonAppService>();
    }

    #endregion
}