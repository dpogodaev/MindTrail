using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MindTrail.Cli.Commands;
using MindTrail.Cli.Constants;
using MindTrail.Cli.Factories;

namespace MindTrail.Cli.Services;

/// <summary>
/// Service that implements the command line interface.
/// </summary>
/// <param name="scopeFactory">Used to create application services within a scope.</param>
public class CliService(IServiceScopeFactory scopeFactory)
    : IHostedService
{
    /// <inheritdoc cref="IHostedService.StartAsync"/>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.Clear();

        using var scope = scopeFactory.CreateScope();

        var commandFactory = new CommandFactory(scope);
        var commandControl = new CommandControl();

        commandFactory
            .Build(CommandFactory.HelpCommandName)
            .Execute(commandControl);

        while (!commandControl.ShouldExit)
        {
            commandFactory
                .Build(GetUserCommand())
                .Execute(commandControl);
        }

        Environment.Exit(0);
        return Task.CompletedTask;
    }

    /// <inheritdoc cref="IHostedService.StopAsync"/>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private static string GetUserCommand()
    {
        Console.ForegroundColor = ColorConstants.InputColor;
        Console.Write("> ");
        var command = Console.ReadLine();
        Console.ResetColor();

        return command;
    }
}