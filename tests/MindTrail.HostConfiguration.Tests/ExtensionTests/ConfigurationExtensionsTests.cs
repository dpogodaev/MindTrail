using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MindTrail.HostConfiguration.Extensions;
using Xunit;

namespace MindTrail.HostConfiguration.Tests.ExtensionTests;

/// <summary>
/// Tests for <see cref="HostConfiguration.Extensions.ConfigurationExtensions"/> class.
/// </summary>
public class ConfigurationExtensionsTests
{
    #region SectionExists

    /// <summary>
    /// Test for <see cref="Extensions.ConfigurationExtensions.SectionExists"/> method.
    /// </summary>
    [Fact]
    public void SectionExists_SpecifiedSectionExists_ReturnsTrue()
    {
        //Arrange
        const string sectionName = "TestSection";

        var configuration = BuildConfiguration(new Dictionary<string, string>
        {
            { $"{sectionName}:Property1", "Test" }
        });

        // Act
        var result = configuration.SectionExists(sectionName);

        // Assert
        Assert.True(result);
    }

    /// <summary>
    /// Test for <see cref="Extensions.ConfigurationExtensions.SectionExists"/> method.
    /// </summary>
    [Fact]
    public void SectionExists_SpecifiedSectionDoesNotExist_ReturnsFalse()
    {
        //Arrange
        const string sectionName = "TestSection";

        var configuration = BuildConfiguration();

        // Act
        var result = configuration.SectionExists(sectionName);

        // Assert
        Assert.False(result);
    }

    #endregion

    #region PropertyExists

    /// <summary>
    /// Test for <see cref="Extensions.ConfigurationExtensions.PropertyExists"/> method.
    /// </summary>
    [Fact]
    public void PropertyExists_SpecifiedPropertyExists_ReturnsTrue()
    {
        //Arrange
        const string propertyName = "TestProperty";

        var configuration = BuildConfiguration(new Dictionary<string, string>
        {
            { propertyName, "Test" }
        });

        // Act
        var result = configuration.PropertyExists(propertyName);

        // Assert
        Assert.True(result);
    }

    /// <summary>
    /// Test for <see cref="Extensions.ConfigurationExtensions.PropertyExists"/> method.
    /// </summary>
    [Fact]
    public void PropertyExists_SpecifiedPropertyDoesNotExist_ReturnsFalse()
    {
        //Arrange
        const string propertyName = "TestProperty";

        var configuration = BuildConfiguration();

        // Act
        var result = configuration.PropertyExists(propertyName);

        // Assert
        Assert.False(result);
    }

    #endregion

    #region GetProperty

    /// <summary>
    /// Test for <see cref="HostConfiguration.Extensions.ConfigurationExtensions.GetProperty"/> method.
    /// </summary>
    [Fact]
    public void GetProperty_SpecifiedPropertyExists_ReturnsPropertyValue()
    {
        //Arrange
        const string propertyName = "TestProperty";
        const string expectedPropertyValue = "Test";

        var configuration = BuildConfiguration(new Dictionary<string, string>
        {
            { propertyName, expectedPropertyValue }
        });

        // Act
        var propertyValue = configuration.GetProperty(propertyName);

        // Assert
        Assert.Equal(expectedPropertyValue, propertyValue);
    }

    /// <summary>
    /// Test for <see cref="HostConfiguration.Extensions.ConfigurationExtensions.GetProperty"/> method.
    /// </summary>
    [Fact]
    public void GetProperty_SpecifiedPropertyDoesNotExist_ReturnsNull()
    {
        //Arrange
        const string propertyName = "TestProperty";

        var configuration = BuildConfiguration();

        // Act
        var propertyValue = configuration.GetProperty(propertyName);

        // Assert
        Assert.Null(propertyValue);
    }

    #endregion

    #region TryGetProperty

    /// <summary>
    /// Test for <see cref="HostConfiguration.Extensions.ConfigurationExtensions.TryGetProperty"/> method.
    /// </summary>
    [Fact]
    public void TryGetProperty_SpecifiedPropertyExists_ReturnsTrue()
    {
        //Arrange
        const string propertyName = "TestProperty";
        const string expectedPropertyValue = "Test";

        var configuration = BuildConfiguration(new Dictionary<string, string>
        {
            { propertyName, expectedPropertyValue }
        });

        // Act
        var propertyExists = configuration.TryGetProperty(propertyName, out var propertyValue);

        // Assert
        Assert.True(propertyExists);
        Assert.Equal(expectedPropertyValue, propertyValue);
    }

    /// <summary>
    /// Test for <see cref="HostConfiguration.Extensions.ConfigurationExtensions.TryGetProperty"/> method.
    /// </summary>
    [Fact]
    public void TryGetProperty_SpecifiedPropertyDoesNotExist_ReturnsFalse()
    {
        //Arrange
        const string propertyName = "TestProperty";

        var configuration = BuildConfiguration();

        // Act
        var propertyExists = configuration.TryGetProperty(propertyName, out _);

        // Assert
        Assert.False(propertyExists);
    }

    #endregion

    #region BindSection

    /// <summary>
    /// Test for <see cref="HostConfiguration.Extensions.ConfigurationExtensions.BindSection{T}"/> method.
    /// </summary>
    [Fact]
    public void BindSection_SpecifiedSectionExists_BindsSectionToSpecifiedClass()
    {
        //Arrange
        const string sectionName = "TestSection";
        const string property1Value = "1";
        const string property2Value = "2";

        var configuration = BuildConfiguration(new Dictionary<string, string>
        {
            { $"{sectionName}:{nameof(TestSection.Property1)}", property1Value },
            { $"{sectionName}:{nameof(TestSection.Property2)}", property2Value }
        });

        // Act
        var specifiedClass = configuration.BindSection<TestSection>(sectionName);

        // Assert
        Assert.NotNull(specifiedClass);
        Assert.Equal(property1Value, specifiedClass.Property1);
        Assert.Equal(property2Value, specifiedClass.Property2);
    }

    /// <summary>
    /// Test for <see cref="HostConfiguration.Extensions.ConfigurationExtensions.BindSection{T}"/> method.
    /// </summary>
    [Fact]
    public void BindSection_SpecifiedSectionDoesNotExist_ReturnsNull()
    {
        //Arrange
        const string sectionName = "TestSection";

        var configuration = BuildConfiguration();

        // Act
        var specifiedClass = configuration.BindSection<TestSection>(sectionName);

        // Assert
        Assert.Null(specifiedClass);
    }

    #endregion

    #region Private methods

    private static IConfiguration BuildConfiguration(Dictionary<string, string> settings = null)
    {
        return new ConfigurationBuilder()
            .AddInMemoryCollection(settings ?? new Dictionary<string, string>())
            .Build();
    }

    #endregion

    #region Private classes

    private record TestSection
    {
        public string Property1 { get; set; }
        public string Property2 { get; set; }
    }

    #endregion
}