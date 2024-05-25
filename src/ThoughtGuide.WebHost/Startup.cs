using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ThoughtGuide.HostConfiguration.Configs.Components;
using ThoughtGuide.HostConfiguration.Interfaces;
using ThoughtGuide.WebHost.Configs.Common;
using ThoughtGuide.WebHost.Configs.Components;
using ThoughtGuide.WebHost.Middlewares;

namespace ThoughtGuide.WebHost;

/// <summary>
/// Application launch configuration.
/// </summary>
internal static class Startup
{
    /// <summary>
    /// Configures all necessary services.
    /// </summary>
    /// <param name="builder">The builder used to register application services.</param>
    /// <param name="configuration">Application configuration.</param>
    /// <param name="logger">The startup logger. Optional.</param>
    public static void ConfigureServices(this IHostApplicationBuilder builder,
        IConfiguration configuration, IStartupLogger logger = null)
    {
        builder.Services.AddCommonConfig();
        builder.Services.AddWebApiConfig(configuration, logger);
        builder.Services.AddWebAuthConfig(configuration, logger);
        builder.Services.AddWebHostConfig();
    }

    /// <summary>
    /// Configures the HTTP request pipeline.
    /// </summary>
    /// <param name="app">The web application used to configure the HTTP pipeline and routes.</param>
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
    /// <param name="host">A host abstraction.</param>
    /// <param name="logger">The startup logger. Optional.</param>
    public static Task ApplyAutoMigrationAsync(this IHost host, IStartupLogger logger)
    {
        //TODO: add a call to the database provider.
        return Task.CompletedTask;
    }
}