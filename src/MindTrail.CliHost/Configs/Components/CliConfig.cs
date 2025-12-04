using Microsoft.Extensions.DependencyInjection;
using MindTrail.Cli.Services;

namespace MindTrail.CliHost.Configs.Components;

/// <summary>
/// Used to configure the component <see cref="MindTrail.Cli"/>.
/// </summary>
internal static class CliConfig
{
    /// <summary>
    /// Adds a configuration for command line interface (hosted services, etc.).
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    public static void AddCliConfig(this IServiceCollection services)
    {
        AddServices(services);
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddHostedService<CliService>();
    }
}