using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.Cli.Helpers;

namespace MindTrail.Cli.Tests.HelperTests;

/// <summary>
/// Tests for <see cref="CommandHelper"/> class.
/// </summary>
[TestClass]
public class CommandHelperTests
{
    #region GetCommandName

    /// <summary>
    /// Test for <see cref="CommandHelper.GetCommandName"/> method.
    /// </summary>
    /// <param name="commandLine">Command line content.</param>
    [TestMethod]
    [DataRow("command-name -t -p 10")]
    [DataRow(" command-name -t  -p   10")]
    public void GetCommandName_CommandLineHasName_ReturnsParsedName(string commandLine)
    {
        // Arrange
        const string expectedName = "command-name";

        // Act
        var name = CommandHelper.GetCommandName(commandLine);

        // Assert
        Assert.AreEqual(expectedName, name);
    }

    /// <summary>
    /// Test for <see cref="CommandHelper.GetCommandName"/> method.
    /// </summary>
    /// <param name="commandLine">Command line content.</param>
    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow("-t")]
    [DataRow(" -t")]
    public void GetCommandName_CommandLineHasNotName_ReturnsNull(string commandLine)
    {
        // Act
        var name = CommandHelper.GetCommandName(commandLine);

        // Assert
        Assert.IsNull(name);
    }

    #endregion

    #region GetCommandOptions

    /// <summary>
    /// Test for <see cref="CommandHelper.GetCommandOptions"/> method.
    /// </summary>
    /// <param name="commandLine">Command line content.</param>
    [TestMethod]
    [DataRow("-t -p 10 --opt1 100 --opt2 'A b c' --opt3 \"d e f\"")]
    [DataRow(" -t  -p  10  --opt1  100 --opt2  'A b c'  --opt3  \"d e f\" ")]
    public void GetCommandOptions_CommandLineHasOptionsWithoutName_ReturnsParsedOptions(string commandLine)
    {
        // Arrange
        var expectedOptions = new Dictionary<string, string>
        {
            { "t", string.Empty },
            { "p", "10" },
            { "opt1", "100" },
            { "opt2", "A b c" },
            { "opt3", "d e f" },
        };

        // Act
        var options = CommandHelper.GetCommandOptions(commandLine, false);

        // Assert
        Assert.AreEqual(expectedOptions["t"], options["t"]);
        Assert.AreEqual(expectedOptions["p"], options["p"]);
        Assert.AreEqual(expectedOptions["opt1"], options["opt1"]);
        Assert.AreEqual(expectedOptions["opt2"], options["opt2"]);
        Assert.AreEqual(expectedOptions["opt3"], options["opt3"]);
    }

    #endregion
}