using System.Collections.Generic;
using MindTrail.Cli.Interfaces;

namespace MindTrail.Cli.Commands.Base;

/// <inheritdoc cref="ICommand"/>
/// <param name="line">Command line.</param>
/// <param name="name">Command name.</param>
/// <param name="options">Command options.</param>
public abstract class Command(string line, string name, Dictionary<string, string> options)
    : ICommand
{
    /// <inheritdoc cref="ICommand.Line"/>
    public string Line { get; } = line;

    /// <inheritdoc cref="ICommand.Name"/>
    public string Name { get; } = name;

    /// <inheritdoc cref="ICommand.Options"/>
    public Dictionary<string, string> Options { get; } = options;

    /// <inheritdoc cref="ICommand.Execute"/>
    public abstract void Execute(CommandControl control);

    /// <summary>
    /// Adds an executable command to the history.
    /// </summary>
    /// <param name="control">Command control.</param>
    protected void AddToHistory(CommandControl control)
    {
        control.ExecutionCounter++;
        control.ExecutionHistory.Add(Line);
    }
}