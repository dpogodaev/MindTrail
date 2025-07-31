using Microsoft.Extensions.DependencyInjection;
using MindTrail.HostConfiguration.Configs.Common;

namespace MindTrail.CliHost.Configs.Components;

/// <summary>
/// Configuration of component <see cref="MindTrail.CliHost"/>.
/// </summary>
internal static class CliHostConfig
{
    /// <summary>
    /// Adds configuration for component <see cref="MindTrail.CliHost"/>.
    /// </summary>
    public static void AddCliHostConfig(this IServiceCollection services)
    {
        services.AddAutomapperConfig();
    }
}