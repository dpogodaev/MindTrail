using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.HostConfiguration.Interfaces.Logging;
using MindTrail.HostConfiguration.Providers;

namespace MindTrail.HostConfiguration.Tests.ProviderTests;

/// <summary>
/// Tests for <see cref="LoggerProvider"/> class.
/// </summary>
[TestClass]
public class LoggerProviderTests
{
    #region Configure

    /// <summary>
    /// Test for <see cref="LoggerProvider.Configure(IConfiguration)"/> method.
    /// </summary>
    [TestMethod]
    public void Configure_Call_LoggerProviderIsConfigured()
    {
        // Arrange
        CreateEmptyFile("NLog.config");
        LoggerProvider.Shutdown();
        var configuration = BuildConfiguration();

        // Act
        LoggerProvider.Configure(configuration);

        // Assert
        Assert.IsTrue(LoggerProvider.IsConfigured());
    }

    /// <summary>
    /// Test for <see cref="LoggerProvider.Configure(IConfiguration)"/> method.
    /// </summary>
    [TestMethod]
    public void Configure_NotCall_LoggerProviderIsNotConfigured()
    {
        // Arrange
        LoggerProvider.Shutdown();

        // Assert
        Assert.IsFalse(LoggerProvider.IsConfigured());
    }

    #endregion

    #region GetStartupLogger

    /// <summary>
    /// Test for <see cref="LoggerProvider.GetStartupLogger"/> method.
    /// </summary>
    [TestMethod]
    public void GetStartupLogger_LoggerProviderIsNotConfigured_ThrowsException()
    {
        // Arrange
        LoggerProvider.Shutdown();

        // Act
        IStartupLogger GetStartupLogger() => LoggerProvider.GetStartupLogger();

        // Assert
        var exception = Assert.Throws<Exception>(GetStartupLogger);
        Assert.AreEqual("The logger provider is not configured", exception.Message);
    }

    #endregion

    private static IConfiguration BuildConfiguration(Dictionary<string, string> settings = null)
    {
        return new ConfigurationBuilder()
            .AddInMemoryCollection(settings ?? new Dictionary<string, string>())
            .Build();
    }

    private static void CreateEmptyFile(string name)
    {
        var filePath = Path.Combine(GetProjectPath(), name);

        if (File.Exists(filePath))
        {
            return;
        }

        var file = File.Create(filePath);
        file.Close();
    }

    private static string GetProjectPath() => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
}