using System.Collections.Generic;

namespace ThoughtGuide.Cli.Commands;

/// <summary>
/// Command execution controller.
/// </summary>
public class CommandControl
{
    /// <summary>
    /// Indicates if to exit the application.
    /// </summary>
    public bool IsExit { get; set; } //TODO

    /// <summary>
    /// Command execution counter.
    /// </summary>
    public int ExecutionCounter { get; set; }

    /// <summary>
    /// Command execution history.
    /// </summary>
    /// <remarks>Remark</remarks>
    public List<string> ExecutionHistory { get; } = [];
}