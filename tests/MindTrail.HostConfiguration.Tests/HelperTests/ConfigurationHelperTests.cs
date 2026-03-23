using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.HostConfiguration.Extensions;
using MindTrail.HostConfiguration.Helpers;

namespace MindTrail.HostConfiguration.Tests.HelperTests;

/// <summary>
/// Tests for <see cref="ConfigurationHelper"/> class.
/// </summary>
[TestClass]
public class ConfigurationHelperTests
{
    #region DetermineRuntimeEnvironment

    /// <summary>
    /// Test for <see cref="ConfigurationHelper.DetermineRuntimeEnvironment"/> method.
    /// </summary>
    [TestMethod]
    public void DetermineRuntimeEnvironment_IsWebHost_ReturnsValueOfAspNetCoreEnvironment()
    {
        // Arrange
        const bool isWebHost = true;

        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");

        // Act
        var runtimeEnvironment = ConfigurationHelper.DetermineRuntimeEnvironment(isWebHost);

        // Assert
        Assert.AreEqual("Development", runtimeEnvironment);
    }

    /// <summary>
    /// Test for <see cref="ConfigurationHelper.DetermineRuntimeEnvironment"/> method.
    /// </summary>
    [TestMethod]
    public void DetermineRuntimeEnvironment_IsNotWebHost_ReturnsValueOfDotNetEnvironment()
    {
        // Arrange
        const bool isWebHost = false;

        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Development");

        // Act
        var runtimeEnvironment = ConfigurationHelper.DetermineRuntimeEnvironment(isWebHost);

        // Assert
        Assert.AreEqual("Development", runtimeEnvironment);
    }

    #endregion

    #region BuildAppConfiguration

    /// <summary>
    /// Test for <see cref="ConfigurationHelper.BuildAppConfiguration"/> method.
    /// </summary>
    [TestMethod]
    public void BuildAppConfiguration_ConfigurationIsSetInAllSources_SetsConfigurationAccordingToPriorityOfSources()
    {
        // Arrange
        const string runtimeEnvironment = "Development";

        WriteToJsonFile("appsettings", new
        {
            TestSection = new
            {
                Property1 = "Property1 from appsettings.json",
                Property2 = "Property2 from appsettings.json",
                Property3 = "Property3 from appsettings.json",
            },
        });

        WriteToJsonFile($"appsettings.{runtimeEnvironment}", new
        {
            TestSection = new
            {
                Property2 = $"Property2 from appsettings.{runtimeEnvironment}.json",
                Property3 = $"Property3 from appsettings.{runtimeEnvironment}.json",
            },
        });

        Environment.SetEnvironmentVariable("TestSection__Property3", "Property3 from EnvironmentVariable");

        // Act
        var appConfig = ConfigurationHelper.BuildAppConfiguration(runtimeEnvironment);

        var property1 = appConfig.GetProperty("TestSection:Property1");
        var property2 = appConfig.GetProperty("TestSection:Property2");
        var property3 = appConfig.GetProperty("TestSection:Property3");

        // Assert
        Assert.IsNotNull(appConfig);

        Assert.AreEqual("Property1 from appsettings.json", property1);
        Assert.AreEqual($"Property2 from appsettings.{runtimeEnvironment}.json", property2);
        Assert.AreEqual("Property3 from EnvironmentVariable", property3);
    }

    #endregion

    private static void WriteToJsonFile(string fileName, object fileContent)
    {
        var path = Path.Combine(GetProjectPath(), $"{fileName}.json");
        var json = JsonSerializer.Serialize(fileContent);

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        File.WriteAllText(path, json);
    }

    private static string GetProjectPath() => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
}