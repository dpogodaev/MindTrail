using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ThoughtGuide.HostConfiguration.Providers;
using ThoughtGuide.WebHost.Interfaces.Services;

namespace ThoughtGuide.WebHost.Services.Hosted;

public class LifetimeEventsHostedService(
    ILogger<LifetimeEventsHostedService> logger,
    IHostApplicationLifetime appLifetime,
    IExecutionService executionManager = null)
    : IHostedService
{
    public static string InstanceId { get; }

    static LifetimeEventsHostedService()
    {
        InstanceId = Guid.NewGuid().ToString();
    }

    #region IHostedService

    public Task StartAsync(CancellationToken cancellationToken)
    {
        appLifetime.ApplicationStopping.Register(OnStopping);

        LoggerProvider.SetInstanceId(InstanceId);

        logger.LogInformation("Starting instance...");

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        LoggerProvider.SetInstanceId(InstanceId);

        logger.LogInformation("Shutting down instance");

        return Task.CompletedTask;
    }

    #endregion

    #region Private methods

    private void OnStopping()
    {
        executionManager?.Shutdown();
    }

    #endregion
}