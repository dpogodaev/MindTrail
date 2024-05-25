using System;
using System.Collections.Generic;
using ThoughtGuide.Cli.Commands.Base;
using ThoughtGuide.Cli.Constants;

namespace ThoughtGuide.Cli.Commands;

/// <summary>
/// The command to display the command execution history.
/// </summary>
/// <param name="line">Command line.</param>
/// <param name="name">Command name.</param>
/// <param name="options">Command options.</param>
public class HistoryCommand(string line, string name, Dictionary<string, string> options) : Command(line, name, options)
{
    #region Command

    /// <inheritdoc cref="Command.Execute"/>
    public override void Execute(CommandControl control)
    {
        Console.ForegroundColor = ColorConstants.OutputColor;

        Console.WriteLine($"Execution counter: {control.ExecutionCounter}");
        Console.WriteLine("Execution history:");
        foreach (var execution in control.ExecutionHistory) Console.WriteLine(execution);

        Console.ResetColor();
    }

    #endregion
}