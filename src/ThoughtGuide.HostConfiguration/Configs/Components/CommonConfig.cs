using Microsoft.Extensions.DependencyInjection;
using ThoughtGuide.Common.Interfaces.Providers;
using ThoughtGuide.Common.Providers;

namespace ThoughtGuide.HostConfiguration.Configs.Components;

/// <summary>
/// Configuration of component <see cref="ThoughtGuide.Common"/>.
/// </summary>
public static class CommonConfig
{
    /// <summary>
    /// Adds configuration for component <see cref="ThoughtGuide.Common"/>.
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