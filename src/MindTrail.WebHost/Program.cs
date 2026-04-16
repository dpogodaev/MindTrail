using System;
using Microsoft.AspNetCore.Builder;
using MindTrail.ApplicationConfigurator.Providers;
using MindTrail.Common.Providers;
using MindTrail.WebHost;

LoggerProvider.Configure(true);
var startupLogger = LoggerProvider.GetStartupLogger();
var timer = new ElapsedTimeMeterProvider().GetElapsedTimeMeter();

try
{
    startupLogger.Debug("The application setup has started");

    timer.Start();
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseLoggerProviderForDI();
    builder.ConfigureServices(builder.Configuration, startupLogger);
    startupLogger.Info("The application's services was successfully configured", timer.ElapsedTimeInMs);

    timer.Restart();
    var app = builder.Build();
    startupLogger.Info("The application was successfully built", timer.ElapsedTimeInMs);

    timer.Restart();
    app.ConfigureHttpRequestPipeline();
    startupLogger.Info("The application's HTTP request pipeline was successfully configured", timer.ElapsedTimeInMs);

    timer.Restart();
    await app.ApplyAutoMigrationAsync(builder.Configuration, startupLogger);
    startupLogger.Info("The application was successfully launched", timer.ElapsedTimeInMs, timer.TotalElapsedTimeInMs);

    timer.Stop();
    await app.RunAsync();
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

/// <summary>
/// The entry point of the application.
/// Configures and runs the web application.
/// </summary>
public partial class Program;