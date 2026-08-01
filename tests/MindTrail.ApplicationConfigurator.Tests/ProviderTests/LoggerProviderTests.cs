using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.ApplicationConfigurator.Interfaces.Logging;
using MindTrail.ApplicationConfigurator.Providers;

namespace MindTrail.ApplicationConfigurator.Tests.ProviderTests;

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
        Assert.AreEqual("The logger provider is not configured.", exception.Message);
    }

    #endregion

    private static IConfiguration BuildConfiguration(Dictionary<string, string>? settings = null)
    {
        var initialData = settings ?? new Dictionary<string, string>();

        return new ConfigurationBuilder()
            .AddInMemoryCollection(initialData!)
            .Build();
    }
}