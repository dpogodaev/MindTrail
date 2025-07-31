using System;
using System.Collections.Generic;
using System.Reflection;
using MindTrail.Cli.Commands.Base;
using MindTrail.Cli.Constants;
using MindTrail.Common.Extensions;

namespace MindTrail.Cli.Commands;

/// <summary>
/// The command to display help information about application.
/// </summary>
/// <param name="line">Command line.</param>
/// <param name="name">Command name.</param>
/// <param name="options">Command options.</param>
public class HelpCommand(string line, string name, Dictionary<string, string> options) : Command(line, name, options)
{
    #region Command

    /// <inheritdoc cref="Command.Execute"/>
    public override void Execute(CommandControl control)
    {
        AddToHistory(control);

        DisplayHelpInfo();
    }

    #endregion

    #region Private methods

    private static void DisplayHelpInfo()
    {
        var assembly = Assembly.GetExecutingAssembly();

        Console.ForegroundColor = ColorConstants.OutputColor;

        Console.WriteLine();
        Console.WriteLine($"Build information:");
        Console.WriteLine($"  Version: {assembly.GetVersion()}");
        Console.WriteLine($"  Date: {assembly.GetAssemblyDate()}");
        Console.WriteLine($"  Name: {assembly.GetAssemblyProductName()}");
        Console.WriteLine($"  Configuration: {assembly.GetAssemblyConfiguration()}");
        Console.WriteLine();
        Console.WriteLine("Commands:");
        Console.WriteLine("  help       Display help information about application");
        Console.WriteLine("  history    The command to display the command execution history");
        Console.WriteLine("  exit       The command to exit the application");
        Console.WriteLine();

        Console.ResetColor();
    }

    #endregion
}