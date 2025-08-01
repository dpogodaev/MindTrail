using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Appsettings.Standard;
using NLog.Config;
using NLog.Extensions.Hosting;
using NLog.Extensions.Logging;
using MindTrail.HostConfiguration.Extensions;

namespace MindTrail.HostConfiguration.Configs.Common;

/// <summary>
/// Used to configure the NLog.
/// </summary>
/// <remarks>
/// The sequence of method calls:
/// 1) <see cref="AddNLogConfig"/>
/// 2) <see cref="UseNLogForDI(IHostBuilder)"/> or <see cref="UseNLogForDI(IServiceCollection)"/>
/// 3) <see cref="ShutdownNLog"/>
/// </remarks>
public static class NLogConfig
{
    private const string NLogConfigParam = "LoggingFeatures:NLog:ConfigFile";
    private const string NLogDefaultConfigFileName = "NLog.config";

    /// <summary>
    /// Adds a configuration for NLog framework.
    /// </summary>
    /// <param name="configuration">The application configuration.</param>
    public static void AddNLogConfig(IConfiguration configuration)
    {
        SetLogManagerConfiguration(configuration);
        SetLogAppSettings(configuration);
    }

    /// <summary>
    /// Setup NLog as logger provider for dependency injection.
    /// </summary>
    /// <param name="builder">The builder used to configure application services.</param>
    /// <remarks>
    /// Adds the use of the NLog as a logger provider for working with <see cref="Microsoft.Extensions.Logging.ILogger{TCategoryName}"/> objects.
    /// </remarks>
    public static void UseNLogForDI(this IHostBuilder builder)
    {
        builder.ConfigureLogging(loggerBuilder => { loggerBuilder.ClearProviders(); });
        builder.UseNLog();
    }

    /// <summary>
    /// Setup NLog as logger provider for dependency injection.
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    /// <remarks>
    /// Adds the use of the NLog as a logger provider for working with <see cref="Microsoft.Extensions.Logging.ILogger{TCategoryName}"/> objects.
    /// </remarks>
    public static void UseNLogForDI(this IServiceCollection services)
    {
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddNLog();
        });
    }

    /// <summary>
    /// Disposes all targets, and shutdowns logging.
    /// </summary>
    public static void ShutdownNLog()
    {
        LogManager.Shutdown();
    }

    /// <summary>
    /// Sets the instance ID in the logical scope context.
    /// </summary>
    /// <param name="id">Instance ID.</param>
    public static void SetInstanceId(string id)
    {
        ScopeContext.PushProperty("instanceId", id);
    }

    #region Private methods

    private static string GetNLogConfigFileName(IConfiguration configuration)
    {
        var nlogConfigFileName = configuration.GetProperty(NLogConfigParam);

        return !string.IsNullOrEmpty(nlogConfigFileName)
            ? nlogConfigFileName
            : NLogDefaultConfigFileName;
    }

    private static void SetLogManagerConfiguration(IConfiguration configuration)
    {
        var nlogConfigFileName = GetNLogConfigFileName(configuration);
        LogManager.Configuration = new XmlLoggingConfiguration(nlogConfigFileName);
    }

    private static void SetLogAppSettings(IConfiguration configuration)
    {
        // It is used to get the application configuration in the 'NLog.config' files.
        // https://www.nuget.org/packages/NLog.Appsettings.Standard
        AppSettingsLayoutRenderer.AppSettings = configuration;
    }

    #endregion
}