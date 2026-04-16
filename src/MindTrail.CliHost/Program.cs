using System;
using Microsoft.Extensions.Hosting;
using MindTrail.ApplicationConfigurator.Providers;
using MindTrail.CliHost;
using MindTrail.Common.Providers;

LoggerProvider.Configure(false);
var startupLogger = LoggerProvider.GetStartupLogger();
var timer = new ElapsedTimeMeterProvider().GetElapsedTimeMeter();

try
{
    startupLogger.Debug("The application setup has started");

    timer.Start();
    var builder = Host.CreateApplicationBuilder(args);
    builder.Services.UseLoggerProviderForDI();
    builder.ConfigureServices(builder.Configuration, logger: startupLogger);

    startupLogger.Info("The application's services was successfully configured", timer.ElapsedTimeInMs);

    timer.Restart();
    var host = builder.Build();

    startupLogger.Info("The application was successfully built", timer.ElapsedTimeInMs);

    timer.Restart();
    await host.ApplyAutoMigrationAsync(builder.Configuration, startupLogger);

    startupLogger.Info("The application was successfully launched", timer.ElapsedTimeInMs, timer.TotalElapsedTimeInMs);

    timer.Stop();
    await host.RunAsync();
}
catch (Exception e)
{
    startupLogger.Error("Failed to launch the application", e);
    throw;
}
finally
{
    LoggerProvider.Shutdown();
}