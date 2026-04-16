using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.ApplicationConfigurator.Extensions;

namespace MindTrail.ApplicationConfigurator.Tests.ExtensionTests;

/// <summary>
/// Tests for <see cref="ApplicationConfigurator.Extensions.ConfigurationExtensions"/> class.
/// </summary>
[TestClass]
public class ConfigurationExtensionsTests
{
    #region SectionExists

    /// <summary>
    /// Test for <see cref="Extensions.ConfigurationExtensions.SectionExists"/> method.
    /// </summary>
    [TestMethod]
    public void SectionExists_SpecifiedSectionExists_ReturnsTrue()
    {
        // Arrange
        const string sectionName = "TestSection";

        var configuration = BuildConfiguration(new Dictionary<string, string>
        {
            { $"{sectionName}:Property1", "Test" },
        });

        // Act
        var result = configuration.SectionExists(sectionName);

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Test for <see cref="Extensions.ConfigurationExtensions.SectionExists"/> method.
    /// </summary>
    [TestMethod]
    public void SectionExists_SpecifiedSectionDoesNotExist_ReturnsFalse()
    {
        // Arrange
        const string sectionName = "TestSection";

        var configuration = BuildConfiguration();

        // Act
        var result = configuration.SectionExists(sectionName);

        // Assert
        Assert.IsFalse(result);
    }

    #endregion

    #region PropertyExists

    /// <summary>
    /// Test for <see cref="Extensions.ConfigurationExtensions.PropertyExists"/> method.
    /// </summary>
    [TestMethod]
    public void PropertyExists_SpecifiedPropertyExists_ReturnsTrue()
    {
        // Arrange
        const string propertyName = "TestProperty";

        var configuration = BuildConfiguration(new Dictionary<string, string>
        {
            { propertyName, "Test" },
        });

        // Act
        var result = configuration.PropertyExists(propertyName);

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Test for <see cref="Extensions.ConfigurationExtensions.PropertyExists"/> method.
    /// </summary>
    [TestMethod]
    public void PropertyExists_SpecifiedPropertyDoesNotExist_ReturnsFalse()
    {
        // Arrange
        const string propertyName = "TestProperty";

        var configuration = BuildConfiguration();

        // Act
        var result = configuration.PropertyExists(propertyName);

        // Assert
        Assert.IsFalse(result);
    }

    #endregion

    #region GetProperty

    /// <summary>
    /// Test for <see cref="ApplicationConfigurator.Extensions.ConfigurationExtensions.GetProperty"/> method.
    /// </summary>
    [TestMethod]
    public void GetProperty_SpecifiedPropertyExists_ReturnsPropertyValue()
    {
        // Arrange
        const string propertyName = "TestProperty";
        const string expectedPropertyValue = "Test";

        var configuration = BuildConfiguration(new Dictionary<string, string>
        {
            { propertyName, expectedPropertyValue },
        });

        // Act
        var propertyValue = configuration.GetProperty(propertyName);

        // Assert
        Assert.AreEqual(expectedPropertyValue, propertyValue);
    }

    /// <summary>
    /// Test for <see cref="ApplicationConfigurator.Extensions.ConfigurationExtensions.GetProperty"/> method.
    /// </summary>
    [TestMethod]
    public void GetProperty_SpecifiedPropertyDoesNotExist_ReturnsNull()
    {
        // Arrange
        const string propertyName = "TestProperty";

        var configuration = BuildConfiguration();

        // Act
        var propertyValue = configuration.GetProperty(propertyName);

        // Assert
        Assert.IsNull(propertyValue);
    }

    #endregion

    #region TryGetProperty

    /// <summary>
    /// Test for <see cref="ApplicationConfigurator.Extensions.ConfigurationExtensions.TryGetProperty"/> method.
    /// </summary>
    [TestMethod]
    public void TryGetProperty_SpecifiedPropertyExists_ReturnsTrue()
    {
        // Arrange
        const string propertyName = "TestProperty";
        const string expectedPropertyValue = "Test";

        var configuration = BuildConfiguration(new Dictionary<string, string>
        {
            { propertyName, expectedPropertyValue },
        });

        // Act
        var propertyExists = configuration.TryGetProperty(propertyName, out var propertyValue);

        // Assert
        Assert.IsTrue(propertyExists);
        Assert.AreEqual(expectedPropertyValue, propertyValue);
    }

    /// <summary>
    /// Test for <see cref="ApplicationConfigurator.Extensions.ConfigurationExtensions.TryGetProperty"/> method.
    /// </summary>
    [TestMethod]
    public void TryGetProperty_SpecifiedPropertyDoesNotExist_ReturnsFalse()
    {
        // Arrange
        const string propertyName = "TestProperty";

        var configuration = BuildConfiguration();

        // Act
        var propertyExists = configuration.TryGetProperty(propertyName, out _);

        // Assert
        Assert.IsFalse(propertyExists);
    }

    #endregion

    #region BindSection

    /// <summary>
    /// Test for <see cref="ApplicationConfigurator.Extensions.ConfigurationExtensions.BindSection{T}"/> method.
    /// </summary>
    [TestMethod]
    public void BindSection_SpecifiedSectionExists_BindsSectionToSpecifiedClass()
    {
        // Arrange
        const string sectionName = "TestSection";
        const string property1Value = "1";
        const string property2Value = "2";

        var configuration = BuildConfiguration(new Dictionary<string, string>
        {
            { $"{sectionName}:{nameof(TestSection.Property1)}", property1Value },
            { $"{sectionName}:{nameof(TestSection.Property2)}", property2Value },
        });

        // Act
        var specifiedClass = configuration.BindSection<TestSection>(sectionName);

        // Assert
        Assert.IsNotNull(specifiedClass);
        Assert.AreEqual(property1Value, specifiedClass.Property1);
        Assert.AreEqual(property2Value, specifiedClass.Property2);
    }

    /// <summary>
    /// Test for <see cref="ApplicationConfigurator.Extensions.ConfigurationExtensions.BindSection{T}"/> method.
    /// </summary>
    [TestMethod]
    public void BindSection_SpecifiedSectionDoesNotExist_ReturnsNull()
    {
        // Arrange
        const string sectionName = "TestSection";

        var configuration = BuildConfiguration();

        // Act
        var specifiedClass = configuration.BindSection<TestSection>(sectionName);

        // Assert
        Assert.IsNull(specifiedClass);
    }

    #endregion

    private static IConfiguration BuildConfiguration(Dictionary<string, string>? settings = null)
    {
        var initialData = settings ?? new Dictionary<string, string>();

        return new ConfigurationBuilder()
            .AddInMemoryCollection(initialData!)
            .Build();
    }

    // ReSharper disable once ClassNeverInstantiated.Local
    private sealed record TestSection
    {
        public string? Property1 { get; set; }

        public string? Property2 { get; set; }
    }
}