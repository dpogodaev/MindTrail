using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using MindTrail.WebHost.Configs.Common;
using MindTrail.WebHost.Tests.ConfigTests.Fakes;
using Xunit;

namespace MindTrail.WebHost.Tests.ConfigTests;

/// <summary>
/// Tests for <see cref="HttpLoggingConfig"/> class.
/// </summary>
public class HttpLoggingConfigTests
{
    #region ConfigureServices

    /// <summary>
    /// Test for <see cref="HttpLoggingConfig.AddHttpLoggingConfig"/> method.
    /// </summary>
    [Fact]
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
        Assert.NotNull(builder);
        Assert.NotNull(mockStartupLogger.WarnMsgList.Single(x =>
            x == "The configuration section 'LoggingFeatures:HttpLogging' is not specified"));
    }

    #endregion

    private static IConfiguration BuildConfiguration(Dictionary<string, string> settings = null)
    {
        return new ConfigurationBuilder()
            .AddInMemoryCollection(settings ?? new Dictionary<string, string>())
            .Build();
    }
}