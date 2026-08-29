using System;
using System.Collections.Generic;
using MindTrail.Cli.Commands.Base;
using MindTrail.Cli.Constants;

namespace MindTrail.Cli.Commands;

/// <summary>
/// The command to display the command execution history.
/// </summary>
/// <param name="line">The command line.</param>
/// <param name="name">The command name.</param>
/// <param name="options">The command options.</param>
public class HistoryCommand(string line, string name, Dictionary<string, string> options)
    : Command(line, name, options)
{
    /// <inheritdoc/>
    public override void Execute(CommandControl control)
    {
        Console.ForegroundColor = ColorConstants.OutputColor;

        Console.WriteLine($"Execution counter: {control.ExecutionCounter}");
        Console.WriteLine("Execution history:");
        foreach (var execution in control.ExecutionHistory)
        {
            Console.WriteLine(execution);
        }

        Console.ResetColor();
    }
}