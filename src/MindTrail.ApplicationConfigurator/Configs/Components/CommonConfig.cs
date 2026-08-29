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
    /// <param name="services">The service collection used to register application services.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance, so that additional calls can be chained.</returns>
    public static IServiceCollection AddCommonConfig(this IServiceCollection services)
    {
        AddProviders(services);

        return services;
    }

    private static void AddProviders(IServiceCollection services)
    {
        services
            .AddTransient<ICurrentTimeProvider, CurrentTimeProvider>()
            .AddTransient<IElapsedTimeMeterProvider, ElapsedTimeMeterProvider>();
    }
}