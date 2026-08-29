using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using MindTrail.Cli.Commands;
using MindTrail.Cli.Helpers;
using MindTrail.Cli.Interfaces;

namespace MindTrail.Cli.Factories;

/// <summary>
/// Command factory.
/// </summary>
/// <param name="scope">The scope used to resolve application services.</param>
public class CommandFactory(IServiceScope scope)
{
    /// <summary>
    /// The name of the help command.
    /// </summary>
    public const string HelpCommandName = "help";

    /// <summary>
    /// The name of the exit command.
    /// </summary>
    public const string ExitCommandName = "exit";

    /// <summary>
    /// The name of the history command.
    /// </summary>
    public const string HistoryCommandName = "history";

    private IServiceScope _scope = scope;

    /// <summary>
    /// Builds the specified command using the command line.
    /// </summary>
    /// <param name="commandLine">The command line.</param>
    /// <returns>The command to execute.</returns>
    public ICommand Build(string? commandLine)
    {
        if (commandLine is null)
        {
            return new EmptyCommand();
        }

        var normalizedCommandLine = commandLine.ToLower();

        var name = CommandHelper.GetCommandName(normalizedCommandLine);

        if (name is null)
        {
            return new UnknownCommand(normalizedCommandLine, string.Empty, new Dictionary<string, string>());
        }

        var options = CommandHelper.GetCommandOptions(normalizedCommandLine.Replace(name, string.Empty), false);

        return name switch
        {
            HelpCommandName => new HelpCommand(normalizedCommandLine, name, options),
            ExitCommandName => new ExitCommand(normalizedCommandLine, name, options),
            HistoryCommandName => new HistoryCommand(normalizedCommandLine, name, options),
            _ => new UnknownCommand(normalizedCommandLine, name, options),
        };
    }
}