using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MindTrail.HostConfiguration.Configs.Common;
using MindTrail.HostConfiguration.Helpers;
using MindTrail.HostConfiguration.Interfaces;
using MindTrail.HostConfiguration.Logging;

namespace MindTrail.HostConfiguration.Providers;

/// <summary>
/// Logger provider for working with <see cref="Microsoft.Extensions.Logging.ILogger{TCategoryName}"/> objects.
/// </summary>
public static class LoggerProvider
{
    private static bool _isConfigured;

    /// <summary>
    /// Indicates if the logger provider is configured.
    /// </summary>
    public static bool IsConfigured() => _isConfigured;

    /// <summary>
    /// Configures the logger provider with the specified application configuration.
    /// </summary>
    /// <param name="configuration">The application configuration.</param>
    public static void Configure(IConfiguration configuration)
    {
        NLogConfig.AddNLogConfig(configuration);

        _isConfigured = true;
    }

    /// <summary>
    /// Configures the logger provider with the default application configuration.
    /// </summary>
    /// <param name="isWebHost">Indicates if it is a web host.</param>
    public static void Configure(bool isWebHost)
    {
        NLogConfig.AddNLogConfig(BuildDefaultConfiguration(isWebHost));

        _isConfigured = true;
    }

    /// <summary>
    /// Shuts down the logger provider.
    /// </summary>
    public static void Shutdown()
    {
        _isConfigured = false;

        NLogConfig.ShutdownNLog();
    }

    /// <summary>
    /// Returns instance of the <see cref="IStartupLogger"/> class.
    /// </summary>
    public static IStartupLogger GetStartupLogger()
    {
        if (!_isConfigured)
        {
            throw new Exception("The logger provider is not configured");
        }

        return new StartupLogger();
    }

    /// <summary>
    /// Setups the logger provider for dependency injection.
    /// </summary>
    /// <param name="builder">The builder used to configure application services.</param>
    /// <remarks>
    /// Adds the use of the logger provider for working with <see cref="Microsoft.Extensions.Logging.ILogger{TCategoryName}"/> objects.
    /// </remarks>
    public static void UseLoggerProviderForDI(this IHostBuilder builder)
    {
        if (!_isConfigured)
        {
            throw new Exception("The logger provider is not configured");
        }

        builder.UseNLogForDI();
    }

    /// <summary>
    /// Setups the logger provider for dependency injection.
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    /// <remarks>
    /// Adds the use of the logger provider for working with <see cref="Microsoft.Extensions.Logging.ILogger{TCategoryName}"/> objects.
    /// </remarks>
    public static void UseLoggerProviderForDI(this IServiceCollection services)
    {
        if (!_isConfigured)
        {
            throw new Exception("The logger provider is not configured");
        }

        services.UseNLogForDI();
    }

    /// <summary>
    /// Sets the instance ID in the scope context.
    /// </summary>
    /// <param name="id">Instance ID.</param>
    public static void SetInstanceId(string id)
    {
        NLogConfig.SetInstanceId(id);
    }

    #region Private methods

    private static IConfiguration BuildDefaultConfiguration(bool isWebHost)
    {
        var runtimeEnvironment = ConfigurationHelper.DetermineRuntimeEnvironment(isWebHost);
        var builtConfiguration = ConfigurationHelper.BuildAppConfiguration(runtimeEnvironment);

        return builtConfiguration;
    }

    #endregion
}