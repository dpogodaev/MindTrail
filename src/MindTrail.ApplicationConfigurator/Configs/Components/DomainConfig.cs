using Microsoft.Extensions.DependencyInjection;

namespace MindTrail.ApplicationConfigurator.Configs.Components;

/// <summary>
/// Used to configure the component <see cref="MindTrail.Domain"/>.
/// </summary>
public static class DomainConfig
{
    /// <summary>
    /// Adds a configuration for domain services.
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    public static void AddDomainServicesConfig(
        this IServiceCollection services)
    {
        services.AddServices();
    }

    private static void AddServices(this IServiceCollection services)
    {
        // Domain services.
    }
}