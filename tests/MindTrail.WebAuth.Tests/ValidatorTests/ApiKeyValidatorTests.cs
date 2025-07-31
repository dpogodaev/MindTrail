using System.Collections.Generic;
using System.Linq;
using MindTrail.WebAuth.Constants;
using MindTrail.WebAuth.Settings;
using MindTrail.WebAuth.Validators;
using Xunit;

namespace MindTrail.WebAuth.Tests.ValidatorTests;

/// <summary>
/// Tests for <see cref="ApiKeyValidator"/> class.
/// </summary>
public class ApiKeyValidatorTests
{
    #region IsValid

    /// <summary>
    /// Test for <see cref="ApiKeyValidator.IsValid"/> method.
    /// </summary>
    [Fact]
    public void IsValid_SingleApiKeyIsValid_ReturnsTrue()
    {
        //Arrange
        const string apiKey = "apiKey";
        var service = BuildService(apiKey);

        // Act
        var result = service.IsValid(apiKey);

        // Assert
        Assert.True(result);
    }

    /// <summary>
    /// Test for <see cref="ApiKeyValidator.IsValid"/> method.
    /// </summary>
    [Fact]
    public void IsValid_SingleApiKeyIsNotValid_ReturnsFalse()
    {
        //Arrange
        const string apiKey = "123";
        var service = BuildService(apiKey);

        // Act
        var result = service.IsValid("not-valid-apikey");

        // Assert
        Assert.False(result);
    }

    /// <summary>
    /// Test for <see cref="ApiKeyValidator.IsValid"/> method.
    /// </summary>
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void IsValid_OneOfAdditionalKeysIsValid_ReturnsTrue(int apiKeyIndex)
    {
        //Arrange
        const string apiKey = "apiKey";

        var additionalApiKeys = new Dictionary<string, string>
        {
            { "user1", "apiKey1" },
            { "user2", "apiKey2" }
        };

        var service = BuildService(apiKey, additionalApiKeys);

        // Act
        var result = service.IsValid(additionalApiKeys.ElementAt(apiKeyIndex).Value);

        // Assert
        Assert.True(result);
    }

    /// <summary>
    /// Test for <see cref="ApiKeyValidator.IsValid"/> method.
    /// </summary>
    [Fact]
    public void IsValid_ApiKeyAndAllAdditionalKeysAreNotValid_ReturnsFalse()
    {
        //Arrange
        const string apiKey = "apiKey";

        var additionalApiKeys = new Dictionary<string, string>
        {
            { "user1", "apiKey1" },
            { "user2", "apiKey2" }
        };

        var service = BuildService(apiKey, additionalApiKeys);

        // Act
        var result = service.IsValid("not-valid-apikey");

        // Assert
        Assert.False(result);
    }

    #endregion

    #region IsNotValid

    /// <summary>
    /// Test for <see cref="ApiKeyValidator.IsNotValid"/> method.
    /// </summary>
    [Fact]
    public void IsNotValid_SingleApiKeyIsValid_ReturnsFalse()
    {
        //Arrange
        const string apiKey = "apiKey";
        var service = BuildService(apiKey);

        // Act
        var result = service.IsNotValid(apiKey);

        // Assert
        Assert.False(result);
    }

    /// <summary>
    /// Test for <see cref="ApiKeyValidator.IsNotValid"/> method.
    /// </summary>
    [Fact]
    public void IsNotValid_SingleApiKeyIsNotValid_ReturnsTrue()
    {
        //Arrange
        const string apiKey = "123";
        var service = BuildService(apiKey);

        // Act
        var result = service.IsNotValid("not-valid-apikey");

        // Assert
        Assert.True(result);
    }

    #endregion

    #region Private methods

    private static ApiKeyValidator BuildService(string apiKey, Dictionary<string, string> additionalApiKeys = null)
    {
        return new ApiKeyValidator(new ApiKeySettings
        {
            ApiKey = apiKey,
            HeaderName = ApiKeyConstants.ApiKeyHeaderName,
            AdditionalApiKeys = additionalApiKeys
        });
    }

    #endregion
}