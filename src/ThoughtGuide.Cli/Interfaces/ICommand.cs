using System.Collections.Generic;
using ThoughtGuide.Cli.Commands;

namespace ThoughtGuide.Cli.Interfaces;

/// <summary>
/// Command to execute.
/// </summary>
public interface ICommand
{
    /// <summary>
    /// Command line.
    /// </summary>
    public string Line { get; }

    /// <summary>
    /// Command name.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Command options.
    /// </summary>
    Dictionary<string, string> Options { get; }

    /// <summary>
    /// Executes the command.
    /// </summary>
    void Execute(CommandControl control);
}