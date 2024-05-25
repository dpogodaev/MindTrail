using System.Collections.Generic;
using ThoughtGuide.Cli.Interfaces;

namespace ThoughtGuide.Cli.Commands.Base;

/// <inheritdoc cref="ICommand"/>
public abstract class Command : ICommand
{
    /// <summary>
    /// Initializes a new instance of <see cref="Command"/> class.
    /// </summary>
    /// <param name="line">Command line.</param>
    /// <param name="name">Command name.</param>
    /// <param name="options">Command options.</param>
    protected Command(string line, string name, Dictionary<string, string> options)
    {
        Line = line;
        Name = name;
        Options = options;
    }

    #region ICommand

    /// <inheritdoc cref="ICommand.Line"/>
    public string Line { get; }

    /// <inheritdoc cref="ICommand.Name"/>
    public string Name { get; }

    /// <inheritdoc cref="ICommand.Options"/>
    public Dictionary<string, string> Options { get; }

    /// <inheritdoc cref="ICommand.Execute"/>
    public abstract void Execute(CommandControl control);

    #endregion

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