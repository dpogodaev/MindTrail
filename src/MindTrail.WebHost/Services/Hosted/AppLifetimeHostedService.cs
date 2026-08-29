using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MindTrail.ApplicationConfigurator.Providers;

namespace MindTrail.WebHost.Services.Hosted;

/// <summary>
/// Handles events related to the application lifetime.
/// </summary>
/// <param name="logger">The logger.</param>
public class AppLifetimeHostedService(ILogger<AppLifetimeHostedService> logger)
    : IHostedService
{
    static AppLifetimeHostedService()
    {
        InstanceId = Guid.NewGuid().ToString();
    }

    /// <summary>
    /// Gets the instance ID.
    /// </summary>
    public static string InstanceId { get; }

    /// <inheritdoc/>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        LoggerProvider.SetInstanceId(InstanceId);

        logger.LogInformation("Launching an instance ...");

        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        LoggerProvider.SetInstanceId(InstanceId);

        logger.LogInformation("Instance shutdown ...");

        return Task.CompletedTask;
    }
}