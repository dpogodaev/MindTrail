using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MindTrail.CliHost.Configs.Components;
using MindTrail.HostConfiguration.Configs.Components;
using MindTrail.HostConfiguration.Interfaces;

namespace MindTrail.CliHost;

/// <summary>
/// Application launch configuration.
/// </summary>
internal static class Startup
{
    /// <summary>
    /// Configures all necessary services.
    /// </summary>
    /// <param name="builder">The builder used to configure application services.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="logger">The startup logger. Optional.</param>
    public static void ConfigureServices(
        this IHostApplicationBuilder builder, IConfiguration configuration, IStartupLogger logger = null)
    {
        builder.Services.AddCommonConfig();
        builder.Services.AddDomainServicesConfig(configuration, logger);
        builder.Services.AddAppServicesConfig(configuration, logger);
        builder.Services.AddEfCoreConfig(configuration, logger);

        builder.Services.AddCliHostConfig();
        builder.Services.AddCliConfig();
    }

    /// <summary>
    /// Applies automatic database migration.
    /// </summary>
    /// <param name="host">The application host.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="logger">The startup logger. Optional.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task ApplyAutoMigrationAsync(
        this IHost host, IConfiguration configuration, IStartupLogger logger)
    {
        await host.ApplyMigrationAsync(configuration, logger);
    }
}