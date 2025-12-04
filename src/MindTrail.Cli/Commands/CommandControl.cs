using System.Collections.Generic;

namespace MindTrail.Cli.Commands;

/// <summary>
/// Command execution controller.
/// </summary>
public class CommandControl
{
    /// <summary>
    /// Gets or sets a value indicating whether indicates if to exit the application.
    /// </summary>
    public bool ShouldExit { get; set; }

    /// <summary>
    /// Gets or sets command execution counter.
    /// </summary>
    public int ExecutionCounter { get; set; }

    /// <summary>
    /// Gets command execution history.
    /// </summary>
    public List<string> ExecutionHistory { get; } = [];
}