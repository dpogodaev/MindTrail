using Microsoft.Extensions.DependencyInjection;
using ThoughtGuide.Cli.Services;

namespace ThoughtGuide.CliHost.Configs.Components;

/// <summary>
/// Configuration of component <see cref="ThoughtGuide.Cli"/>.
/// </summary>
internal static class CliConfig
{
    /// <summary>
    /// Adds configuration for component <see cref="ThoughtGuide.Cli"/>.
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