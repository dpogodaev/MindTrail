using Microsoft.Extensions.DependencyInjection;

namespace MindTrail.CliHost.Configs.Components;

/// <summary>
/// Used to configure the component <see cref="MindTrail.CliHost"/>.
/// </summary>
internal static class CliHostConfig
{
    /// <summary>
    /// Adds a configuration for the CLI host (hosted services, providers, adapters, etc.).
    /// </summary>
    /// <param name="services">The service collection used to register application services.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance, so that additional calls can be chained.</returns>
    public static IServiceCollection AddCliHostConfig(this IServiceCollection services)
    {
        return services;
    }
}