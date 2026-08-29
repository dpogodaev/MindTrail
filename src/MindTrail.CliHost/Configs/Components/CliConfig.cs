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
    /// <param name="services">The service collection used to register application services.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance, so that additional calls can be chained.</returns>
    public static IServiceCollection AddCliConfig(this IServiceCollection services)
    {
        AddServices(services);

        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddHostedService<CliService>();
    }
}