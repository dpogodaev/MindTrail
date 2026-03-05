using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MindTrail.HostConfiguration.Configs.Components;
using MindTrail.HostConfiguration.Interfaces.Logging;
using MindTrail.WebHost.Configs.Common;
using MindTrail.WebHost.Configs.Components;
using MindTrail.WebHost.Middlewares;

namespace MindTrail.WebHost;

/// <summary>
/// Application launch configuration.
/// </summary>
internal static class Startup
{
    /// <summary>
    /// Configures all necessary services.
    /// </summary>
    /// <param name="builder">The builder used to register application services.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="logger">The startup logger. Optional.</param>
    public static void ConfigureServices(
        this IHostApplicationBuilder builder,
        IConfiguration configuration,
        IStartupLogger? logger = null)
    {
        builder.Services.AddCommonConfig();
        builder.Services.AddDomainServicesConfig();
        builder.Services.AddAppServicesConfig();
        builder.Services.AddEfCoreConfig(configuration, logger);

        builder.Services.AddWebHostConfig();
        builder.Services.AddWebAuthConfig(configuration, logger);
        builder.Services.AddWebApiConfig(configuration, logger);
    }

    /// <summary>
    /// Configures the HTTP request pipeline.
    /// </summary>
    /// <param name="app">Used to configure the HTTP pipeline and routes.</param>
    public static void ConfigureHttpRequestPipeline(this WebApplication app)
    {
        app.MapHealthChecks("/health");
        app.UseMiddleware<SettingInstanceIdMiddleware>();
        app.UseExceptionHandler();
        app.UseHttpLogging();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.UseSwagger();
        app.UseSwaggerUI(SwaggerConfig.ConfigureSwaggerUI);
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