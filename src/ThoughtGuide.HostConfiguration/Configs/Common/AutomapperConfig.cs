using Microsoft.Extensions.DependencyInjection;
using ThoughtGuide.HostConfiguration.Helpers;

namespace ThoughtGuide.HostConfiguration.Configs.Common;

/// <summary>
/// Automapper configuration.
/// </summary>
public static class AutomapperConfig
{
    private const string SolutionName = "ThoughtGuide";

    /// <summary>
    /// Adds a configuration for automapper.
    /// </summary>
    public static void AddAutomapperConfig(this IServiceCollection services)
    {
        services.AddAutoMapper(x =>
        {
            x.AllowNullCollections = true;
            x.AddMaps(AssemblyHelper.GetAssemblyNames(SolutionName));
        });
    }
}