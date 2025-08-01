using System.Collections.Generic;

namespace MindTrail.Cli.Commands;

/// <summary>
/// Command execution controller.
/// </summary>
public class CommandControl
{
    /// <summary>
    /// Indicates if to exit the application.
    /// </summary>
    public bool ShouldExit { get; set; }

    /// <summary>
    /// Command execution counter.
    /// </summary>
    public int ExecutionCounter { get; set; }

    /// <summary>
    /// Command execution history.
    /// </summary>
    public List<string> ExecutionHistory { get; } = [];
}