using System.Collections.Generic;

namespace MindTrail.Cli.Commands;

/// <summary>
/// Command execution controller.
/// </summary>
public class CommandControl
{
    /// <summary>
    /// Gets or sets a value indicating whether the application should exit.
    /// </summary>
    public bool ShouldExit { get; set; }

    /// <summary>
    /// Gets or sets the command execution counter.
    /// </summary>
    public int ExecutionCounter { get; set; }

    /// <summary>
    /// Gets the command execution history.
    /// </summary>
    public List<string> ExecutionHistory { get; } = [];
}