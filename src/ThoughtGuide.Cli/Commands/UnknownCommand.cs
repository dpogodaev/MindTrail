using System;
using System.Collections.Generic;
using ThoughtGuide.Cli.Commands.Base;
using ThoughtGuide.Cli.Constants;

namespace ThoughtGuide.Cli.Commands;

/// <summary>
/// The command that could not be executed.
/// </summary>
/// <param name="line">Command line.</param>
/// <param name="name">Command name.</param>
/// <param name="options">Command options.</param>
public class UnknownCommand(string line, string name, Dictionary<string, string> options)
    : Command(line, name, options)
{
    #region Command

    /// <inheritdoc cref="Command.Execute"/>
    public override void Execute(CommandControl control)
    {
        Console.ForegroundColor = ColorConstants.ErrorOutputColor;
        Console.WriteLine("Could not execute because the specified command was not found. See 'help'.");
        Console.ResetColor();
    }

    #endregion
}