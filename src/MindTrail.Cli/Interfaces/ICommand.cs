using System.Collections.Generic;
using MindTrail.Cli.Commands;

namespace MindTrail.Cli.Interfaces;

/// <summary>
/// Command to execute.
/// </summary>
public interface ICommand
{
    /// <summary>
    /// Gets the command line.
    /// </summary>
    public string Line { get; }

    /// <summary>
    /// Gets the name of the command.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the command options.
    /// </summary>
    Dictionary<string, string> Options { get; }

    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="control">Command execution controller.</param>
    void Execute(CommandControl control);
}