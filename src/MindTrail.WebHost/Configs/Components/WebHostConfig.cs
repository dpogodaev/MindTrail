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
    /// Adds a configuration for the web host (hosted services, providers, adapters, etc.).
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance, so that additional calls can be chained.</returns>
    public static IServiceCollection AddWebHostConfig(this IServiceCollection services)
    {
        AddProviders(services);
        AddHostedServicesConfig(services);

        return services;
    }

    private static void AddProviders(IServiceCollection services)
    {
        services
            .AddSingleton<TraceIdProvider>()
            .AddSingleton<ErrorCodeProvider>()
            .AddSingleton<ProblemInstanceProvider>();
    }

    private static void AddHostedServicesConfig(IServiceCollection services)
    {
        services.AddHostedService<AppLifetimeHostedService>();
    }
}