using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ThoughtGuide.CliHost.Configs.Components;
using ThoughtGuide.HostConfiguration.Configs.Components;
using ThoughtGuide.HostConfiguration.Interfaces;

namespace ThoughtGuide.CliHost;

/// <summary>
/// Application launch configuration.
/// </summary>
internal static class Startup
{
    /// <summary>
    /// Configures all necessary services.
    /// </summary>
    /// <param name="builder">The builder used to configure application services.</param>
    /// <param name="configuration">Application configuration.</param>
    /// <param name="logger">The startup logger. Optional.</param>
    public static void ConfigureServices(this IHostApplicationBuilder builder,
        IConfiguration configuration, IStartupLogger logger = null)
    {
        builder.Services.AddCommonConfig();
        builder.Services.AddCliConfig();
        builder.Services.AddCliHostConfig();
    }

    /// <summary>
    /// Applies automatic database migration.
    /// </summary>
    /// <param name="host">A host abstraction.</param>
    /// <param name="logger">The startup logger. Optional.</param>
    public static Task ApplyAutoMigrationAsync(this IHost host, IStartupLogger logger)
    {
        //TODO: add a call to the database provider
        return Task.CompletedTask;
    }
}