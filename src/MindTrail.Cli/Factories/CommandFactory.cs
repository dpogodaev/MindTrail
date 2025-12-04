using Microsoft.Extensions.DependencyInjection;
using MindTrail.Cli.Commands;
using MindTrail.Cli.Helpers;
using MindTrail.Cli.Interfaces;

namespace MindTrail.Cli.Factories;

/// <summary>
/// Command factory.
/// </summary>
/// <param name="scope">Used to create application services within a scope.</param>
public class CommandFactory(IServiceScope scope)
{
    /// <summary>
    /// The name of the command for getting help.
    /// </summary>
    public const string HelpCommandName = "help";

    /// <summary>
    /// The name of the exit command.
    /// </summary>
    public const string ExitCommandName = "exit";

    /// <summary>
    /// The name of the command for getting the history.
    /// </summary>
    public const string HistoryCommandName = "history";

    private IServiceScope _scope = scope;

    /// <summary>
    /// Builds the specified command using the command line.
    /// </summary>
    /// <param name="commandLine">Command line.</param>
    /// <returns>The command to execute.</returns>
    public ICommand Build(string commandLine)
    {
        var parsedCommandLine = commandLine.ToLower();

        var name = CommandHelper.GetCommandName(parsedCommandLine);
        var options = CommandHelper.GetCommandOptions(parsedCommandLine.Replace(name, string.Empty), false);

        return name switch
        {
            HelpCommandName => new HelpCommand(parsedCommandLine, name, options),
            ExitCommandName => new ExitCommand(parsedCommandLine, name, options),
            HistoryCommandName => new HistoryCommand(parsedCommandLine, name, options),
            _ => new UnknownCommand(parsedCommandLine, name, options),
        };
    }
}