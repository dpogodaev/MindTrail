using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.WebHost.Configs.Common;
using MindTrail.WebHost.Tests.ConfigTests.Fakes;

namespace MindTrail.WebHost.Tests.ConfigTests;

/// <summary>
/// Tests for <see cref="HttpLoggingConfig"/> class.
/// </summary>
[TestClass]
public class HttpLoggingConfigTests
{
    #region ConfigureServices

    /// <summary>
    /// Test for <see cref="HttpLoggingConfig.AddHttpLoggingConfig"/> method.
    /// </summary>
    [TestMethod]
    public void ConfigureServices_HttpLoggingSectionIsNotSpecified_LogsWarningMsg()
    {
        // Arrange
        var settings = new Dictionary<string, string>
        {
            { "LoggingFeatures", string.Empty },
        };

        var mockStartupLogger = new FakeStartupLogger();
        var builder = WebApplication.CreateBuilder();
        var configuration = BuildConfiguration(settings);

        // Act
        builder.Services.AddHttpLoggingConfig(configuration, mockStartupLogger);

        // Assert
        Assert.IsNotNull(builder);
        Assert.IsNotNull(mockStartupLogger.WarnMsgList.Single(x =>
            x == "The configuration section 'LoggingFeatures:HttpLogging' is not specified"));
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