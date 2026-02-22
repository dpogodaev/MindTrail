using Microsoft.Extensions.DependencyInjection;
using MindTrail.WebHost.Abstractions.Providers;
using MindTrail.WebHost.Services.Hosted;

namespace MindTrail.WebHost.Configs.Components;

/// <summary>
/// Used to configure the component <see cref="MindTrail.WebHost"/>.
/// </summary>
internal static class WebHostConfig
{
    /// <summary>
    /// Adds a configuration for the web host (infrastructure services, providers, adapters, etc.).
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    public static void AddWebHostConfig(this IServiceCollection services)
    {
        AddProviders(services);
        AddHostedServicesConfig(services);
    }

    private static void AddProviders(IServiceCollection services)
    {
        services.AddSingleton<ErrorCodeProvider>();
        services.AddSingleton<ProblemInstanceProvider>();
    }

    private static void AddHostedServicesConfig(IServiceCollection services)
    {
        services.AddHostedService<AppLifetimeHostedService>();
    }
}