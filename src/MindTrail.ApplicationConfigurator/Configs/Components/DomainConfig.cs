using Microsoft.Extensions.DependencyInjection;

namespace MindTrail.ApplicationConfigurator.Configs.Components;

/// <summary>
/// Used to configure the component <see cref="MindTrail.Domain"/>.
/// </summary>
public static class DomainConfig
{
    /// <summary>
    /// Adds a configuration for domain core.
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance, so that additional calls can be chained.</returns>
    public static IServiceCollection AddDomainServicesConfig(this IServiceCollection services)
    {
        AddServices(services);

        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        // Domain services.
    }
}