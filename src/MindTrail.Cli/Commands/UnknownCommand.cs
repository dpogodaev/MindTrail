using System;
using System.Collections.Generic;
using MindTrail.Cli.Commands.Base;
using MindTrail.Cli.Constants;

namespace MindTrail.Cli.Commands;

/// <summary>
/// The unknown command that could not be executed.
/// </summary>
/// <param name="line">Command line.</param>
/// <param name="name">Command name.</param>
/// <param name="options">Command options.</param>
public class UnknownCommand(string line, string name, Dictionary<string, string> options)
    : Command(line, name, options)
{
    /// <inheritdoc cref="Command.Execute"/>
    public override void Execute(CommandControl control)
    {
        Console.ForegroundColor = ColorConstants.ErrorOutputColor;
        Console.WriteLine("Could not execute because the specified command was not found. Type 'help' to see available commands.");
        Console.ResetColor();
    }
}