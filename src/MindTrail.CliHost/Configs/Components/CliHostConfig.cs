using Microsoft.Extensions.DependencyInjection;

namespace MindTrail.CliHost.Configs.Components;

/// <summary>
/// Used to configure the component <see cref="MindTrail.CliHost"/>.
/// </summary>
internal static class CliHostConfig
{
    /// <summary>
    /// Adds a configuration for the CLI host (infrastructure services, providers, adapters, etc.).
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    public static void AddCliHostConfig(this IServiceCollection services)
    {
    }
}