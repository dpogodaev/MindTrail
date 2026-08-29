using System;
using System.Collections.Generic;
using MindTrail.Cli.Commands.Base;
using MindTrail.Cli.Constants;

namespace MindTrail.Cli.Commands;

/// <summary>
/// The command to exit the application.
/// </summary>
/// <param name="line">The command line.</param>
/// <param name="name">The command name.</param>
/// <param name="options">The command options.</param>
public class ExitCommand(string line, string name, Dictionary<string, string> options)
    : Command(line, name, options)
{
    /// <inheritdoc/>
    public override void Execute(CommandControl control)
    {
        control.ShouldExit = true;

        Console.ForegroundColor = ColorConstants.OutputColor;
        Console.WriteLine("Exit...");
        Console.ResetColor();
    }
}