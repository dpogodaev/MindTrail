using System.Collections.Generic;
using ThoughtGuide.Cli.Helpers;
using Xunit;

namespace ThoughtGuide.Cli.Tests.HelperTests;

/// <summary>
/// Tests for <see cref="CommandHelper"/> class.
/// </summary>
public class CommandHelperTests
{
    #region GetCommandName

    /// <summary>
    /// Test for <see cref="CommandHelper.GetCommandName"/> method.
    /// </summary>
    [Theory]
    [InlineData("command-name -t -p 10")]
    [InlineData(" command-name -t  -p   10")]
    public void GetCommandName_CommandLineHasName_ReturnsParsedName(string commandLine)
    {
        // Arrange
        const string expectedName = "command-name";

        // Act
        var name = CommandHelper.GetCommandName(commandLine);

        // Assert
        Assert.Equal(expectedName, name);
    }

    /// <summary>
    /// Test for <see cref="CommandHelper.GetCommandName"/> method.
    /// </summary>
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("-t")]
    [InlineData(" -t")]
    public void GetCommandName_CommandLineHasNotName_ReturnsNull(string commandLine)
    {
        // Act
        var name = CommandHelper.GetCommandName(commandLine);

        // Assert
        Assert.Null(name);
    }

    #endregion

    #region GetCommandOptions

    /// <summary>
    /// Test for <see cref="CommandHelper.GetCommandOptions"/> method.
    /// </summary>
    [Theory]
    [InlineData("-t -p 10 --opt1 100 --opt2 'a b c' --opt3 \"d e f\"")]
    [InlineData(" -t  -p  10  --opt1  100 --opt2  'a b c'  --opt3  \"d e f\" ")]
    public void GetCommandOptions_CommandLineHasOptionsWithoutName_ReturnsParsedOptions(string commandLine)
    {
        // Arrange
        var expectedOptions = new Dictionary<string, string>
        {
            { "t", "" },
            { "p", "10" },
            { "opt1", "100" },
            { "opt2", "a b c" },
            { "opt3", "d e f" }
        };

        // Act
        var options = CommandHelper.GetCommandOptions(commandLine, false);

        // Assert
        Assert.Equal(expectedOptions, options);
    }

    #endregion
}