using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ThoughtGuide.Cli.Commands;
using ThoughtGuide.Cli.Constants;
using ThoughtGuide.Cli.Factories;

namespace ThoughtGuide.Cli.Services;

/// <summary>
/// Service that implements the command line interface.
/// </summary>
/// <param name="scopeFactory">Used to create application services within a scope.</param>
public class CliService(IServiceScopeFactory scopeFactory) : IHostedService
{
    #region IHostedService

    /// <inheritdoc cref="IHostedService.StartAsync"/>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.Clear();

        using var scope = scopeFactory.CreateScope();

        var commandFactory = new CommandFactory(scope);
        var commandControl = new CommandControl();

        while (!commandControl.IsExit)
        {
            commandFactory.Build(GetCommandLine()).Execute(commandControl);
        }

        Environment.Exit(0);
        return Task.CompletedTask;
    }

    /// <inheritdoc cref="IHostedService.StopAsync"/>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Private methods

    private static string GetCommandLine()
    {
        Console.ForegroundColor = ColorConstants.InputColor;
        Console.Write("> ");
        var command = Console.ReadLine();
        Console.ResetColor();

        return command;
    }

    #endregion
}