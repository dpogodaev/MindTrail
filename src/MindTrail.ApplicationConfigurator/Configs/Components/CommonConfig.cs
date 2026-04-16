using Microsoft.Extensions.DependencyInjection;
using MindTrail.Common.Interfaces.Providers;
using MindTrail.Common.Providers;

namespace MindTrail.ApplicationConfigurator.Configs.Components;

/// <summary>
/// Used to configure the component <see cref="MindTrail.Common"/>.
/// </summary>
public static class CommonConfig
{
    /// <summary>
    /// Adds a configuration for all types of shared resources (providers, helpers, extensions, utils, etc.).
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    public static void AddCommonConfig(
        this IServiceCollection services)
    {
        services.AddProviders();
    }

    private static void AddProviders(this IServiceCollection services)
    {
        services
            .AddTransient<ICurrentTimeProvider, CurrentTimeProvider>()
            .AddTransient<IElapsedTimeMeterProvider, ElapsedTimeMeterProvider>();
    }
}