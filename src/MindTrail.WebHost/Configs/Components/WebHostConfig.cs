using Microsoft.Extensions.DependencyInjection;
using MindTrail.HostConfiguration.Configs.Common;
using MindTrail.WebHost.Configs.Common;

namespace MindTrail.WebHost.Configs.Components;

/// <summary>
/// Configuration of component <see cref="MindTrail.WebHost"/>.
/// </summary>
internal static class WebHostConfig
{
    /// <summary>
    /// Adds configuration for component <see cref="MindTrail.WebHost"/>.
    /// </summary>
    public static void AddWebHostConfig(this IServiceCollection services)
    {
        services.AddAutomapperConfig();
    }
}