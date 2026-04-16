using System;
using System.Collections.Generic;
using MindTrail.Cli.Commands.Base;
using MindTrail.Cli.Constants;

namespace MindTrail.Cli.Commands;

/// <summary>
/// The empty command that could not be executed.
/// </summary>
public class EmptyCommand()
    : Command(string.Empty, string.Empty, new Dictionary<string, string>())
{
    /// <inheritdoc cref="Command.Execute"/>
    public override void Execute(CommandControl control)
    {
        Console.ForegroundColor = ColorConstants.ErrorOutputColor;
        Console.WriteLine("No command specified. Type 'help' to see available commands.");
        Console.ResetColor();
    }
}