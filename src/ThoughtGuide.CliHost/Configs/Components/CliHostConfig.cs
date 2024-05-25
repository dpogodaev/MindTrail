using Microsoft.Extensions.DependencyInjection;
using ThoughtGuide.HostConfiguration.Configs.Common;

namespace ThoughtGuide.CliHost.Configs.Components;

/// <summary>
/// Configuration of component <see cref="ThoughtGuide.CliHost"/>.
/// </summary>
internal static class CliHostConfig
{
    /// <summary>
    /// Adds configuration for component <see cref="ThoughtGuide.CliHost"/>.
    /// </summary>
    public static void AddCliHostConfig(this IServiceCollection services)
    {
        services.AddAutomapperConfig();
    }
}