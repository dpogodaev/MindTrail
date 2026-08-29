using System.Collections.Generic;
using MindTrail.Cli.Interfaces;

namespace MindTrail.Cli.Commands.Base;

/// <inheritdoc/>
/// <param name="line">The command line.</param>
/// <param name="name">The command name.</param>
/// <param name="options">The command options.</param>
public abstract class Command(string line, string name, Dictionary<string, string> options)
    : ICommand
{
    /// <inheritdoc/>
    public string Line { get; } = line;

    /// <inheritdoc/>
    public string Name { get; } = name;

    /// <inheritdoc/>
    public Dictionary<string, string> Options { get; } = options;

    /// <inheritdoc/>
    public abstract void Execute(CommandControl control);

    /// <summary>
    /// Adds an executable command to the history.
    /// </summary>
    /// <param name="control">The command execution controller.</param>
    protected void AddToHistory(CommandControl control)
    {
        control.ExecutionCounter++;
        control.ExecutionHistory.Add(Line);
    }
}