using System;
using System.Collections.Generic;
using MindTrail.Cli.Commands.Base;
using MindTrail.Cli.Constants;

namespace MindTrail.Cli.Commands;

/// <summary>
/// The unknown command that could not be executed.
/// </summary>
/// <param name="line">The command line.</param>
/// <param name="name">The command name.</param>
/// <param name="options">The command options.</param>
public class UnknownCommand(string line, string name, Dictionary<string, string> options)
    : Command(line, name, options)
{
    /// <inheritdoc/>
    public override void Execute(CommandControl control)
    {
        Console.ForegroundColor = ColorConstants.ErrorOutputColor;
        Console.WriteLine("Could not execute because the specified command was not found. Type 'help' to see available commands.");
        Console.ResetColor();
    }
}