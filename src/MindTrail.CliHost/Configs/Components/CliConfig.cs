using Microsoft.Extensions.DependencyInjection;
using MindTrail.Cli.Services;

namespace MindTrail.CliHost.Configs.Components;

/// <summary>
/// Configuration of component <see cref="MindTrail.Cli"/>.
/// </summary>
internal static class CliConfig
{
    /// <summary>
    /// Adds configuration for component <see cref="MindTrail.Cli"/>.
    /// </summary>
    public static void AddCliConfig(this IServiceCollection services)
    {
        AddServices(services);
    }

    #region Private methods

    private static void AddServices(IServiceCollection services)
    {
        services.AddHostedService<CliService>();
    }

    #endregion
}