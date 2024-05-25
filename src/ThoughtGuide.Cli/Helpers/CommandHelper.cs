using System.Collections.Generic;
using System.Text;

namespace ThoughtGuide.Cli.Helpers;

/// <summary>
/// Command helper.
/// </summary>
public class CommandHelper
{
    /// <summary>
    /// Returns the command name from the command line.
    /// </summary>
    /// <param name="commandLine">Command line.</param>
    /// <returns>The command name.</returns>
    public static string GetCommandName(string commandLine)
    {
        var splittedLine = commandLine.Trim().Split(" ");
        var commandName = splittedLine[0].Trim();

        if (splittedLine.Length == 1 && string.IsNullOrEmpty(commandName))
        {
            return null;
        }

        return commandName.StartsWith('-') ? null : commandName;
    }

    /// <summary>
    /// Returns the command options from the command line.
    /// </summary>
    /// <param name="commandLine">Command line.</param>
    /// <param name="withCommandName">Indicates if the command name is on the command line.</param>
    /// <returns>Command options.</returns>
    public static Dictionary<string, string> GetCommandOptions(string commandLine, bool withCommandName = true)
    {
        var options = new Dictionary<string, string>();

        const char startMarker = '-';
        const char defaultEndMarker = ' ';
        var endMarker = ' ';
        var isStartMarker = false;
        var isName = false;
        var isValue = false;

        var name = new StringBuilder();
        var value = new StringBuilder();

        void AddOption()
        {
            options[name.ToString().Trim()] = value.ToString().Trim();

            endMarker = defaultEndMarker;
            isName = false;
            isValue = false;

            name.Clear();
            value.Clear();
        }

        var commandLineWithoutCommandName = withCommandName
            ? commandLine.Replace(GetCommandName(commandLine), "")
            : commandLine;

        var parsedCommandLine = commandLineWithoutCommandName.Replace("\"", "'").Trim() + defaultEndMarker;

        foreach (var commandSymbol in parsedCommandLine)
        {
            if (commandSymbol == startMarker)
            {
                if (isValue) AddOption(); // when option without value
                isStartMarker = true;
                continue;
            }

            if (isStartMarker)
            {
                isStartMarker = false;
                isName = true;
            }

            if (isName)
            {
                if (commandSymbol == endMarker)
                {
                    isName = false;
                    isValue = true;
                    continue;
                }

                name.Append(commandSymbol);
                continue;
            }

            if (isValue)
            {
                if (commandSymbol == ' ' && value.Length == 0) continue;

                if (commandSymbol == '\'' && value.Length == 0)
                {
                    endMarker = '\'';
                    continue;
                }

                if (commandSymbol == endMarker)
                {
                    AddOption();
                    continue;
                }

                value.Append(commandSymbol);
            }
        }

        return options;
    }
}