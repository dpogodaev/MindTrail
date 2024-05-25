using Microsoft.Extensions.DependencyInjection;
using ThoughtGuide.Cli.Commands;
using ThoughtGuide.Cli.Helpers;
using ThoughtGuide.Cli.Interfaces;

namespace ThoughtGuide.Cli.Factories;

/// <summary>
/// Command factory.
/// </summary>
/// <param name="scope">Used to create application services within a scope.</param>
public class CommandFactory(IServiceScope scope)
{
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
        var options = CommandHelper.GetCommandOptions(parsedCommandLine.Replace(name, ""), false);

        return name switch
        {
            "help" => new HelpCommand(parsedCommandLine, name, options),
            "exit" => new ExitCommand(parsedCommandLine, name, options),
            "history" => new HistoryCommand(parsedCommandLine, name, options),
            _ => new UnknownCommand(parsedCommandLine, name, options)
        };
    }
}