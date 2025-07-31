using Microsoft.Extensions.DependencyInjection;
using MindTrail.Common.Interfaces.Providers;
using MindTrail.Common.Providers;

namespace MindTrail.HostConfiguration.Configs.Components;

/// <summary>
/// Configuration of component <see cref="MindTrail.Common"/>.
/// </summary>
public static class CommonConfig
{
    /// <summary>
    /// Adds configuration for component <see cref="MindTrail.Common"/>.
    /// </summary>
    public static void AddCommonConfig(this IServiceCollection services)
    {
        AddProviders(services);
    }

    #region Private methods

    private static void AddProviders(IServiceCollection services)
    {
        services.AddTransient<ICurrentTimeProvider, CurrentTimeProvider>();
        services.AddTransient<IElapsedTimeMeterProvider, ElapsedTimeMeterProvider>();
    }

    #endregion
}