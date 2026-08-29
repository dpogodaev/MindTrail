using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MindTrail.ApplicationConfigurator.Configs.Components;
using MindTrail.ApplicationConfigurator.Interfaces.Logging;
using MindTrail.CliHost.Configs.Components;

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
        this IHostApplicationBuilder builder, IConfiguration configuration, IStartupLogger? logger = null)
    {
        builder.Services
            .AddCommonConfig()
            .AddDomainServicesConfig()
            .AddApplicationConfig()
            .AddApplicationContractsConfig()
            .AddEfCoreConfig(configuration, logger);

        builder.Services
            .AddCliHostConfig()
            .AddCliConfig();
    }

    /// <summary>
    /// Applies automatic database migration.
    /// </summary>
    /// <param name="host">The application host.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="logger">The startup logger.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task ApplyAutoMigrationAsync(
        this IHost host,
        IConfiguration configuration,
        IStartupLogger logger)
    {
        await host.ApplyMigrationAsync(configuration, logger);
    }
}