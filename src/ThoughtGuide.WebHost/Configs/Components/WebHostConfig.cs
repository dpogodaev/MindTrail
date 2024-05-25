using Microsoft.Extensions.DependencyInjection;
using ThoughtGuide.HostConfiguration.Configs.Common;
using ThoughtGuide.WebHost.Configs.Common;

namespace ThoughtGuide.WebHost.Configs.Components;

/// <summary>
/// Configuration of component <see cref="ThoughtGuide.WebHost"/>.
/// </summary>
internal static class WebHostConfig
{
    /// <summary>
    /// Adds configuration for component <see cref="ThoughtGuide.WebHost"/>.
    /// </summary>
    public static void AddWebHostConfig(this IServiceCollection services)
    {
        services.AddAutomapperConfig();
    }
}