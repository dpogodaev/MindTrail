using Microsoft.AspNetCore.Http;
using MindTrail.WebAuth.Extensions;
using Xunit;

namespace MindTrail.WebAuth.Tests.ExtensionTests;

/// <summary>
/// Tests for <see cref="HttpRequestExtensions"/> class.
/// </summary>
public class HttpRequestExtensionsTests
{
    #region GetHeaderKeyValue

    /// <summary>
    /// Test for <see cref="HttpRequestExtensions.GetHeaderKeyValue"/> method.
    /// </summary>
    [Fact]
    public void GetHeaderKeyValue_KeyExists_ReturnsKeyValue()
    {
        // Arrange
        const string keyName = "name";
        const string keyValue = "value";

        var request = BuildHttpRequest();
        request.Headers.Append(keyName, keyValue);

        // Act
        var result = request.GetHeaderKeyValue(keyName);

        // Assert
        Assert.Equal(keyValue, result);
    }

    /// <summary>
    /// Test for <see cref="HttpRequestExtensions.GetHeaderKeyValue"/> method.
    /// </summary>
    [Fact]
    public void GetHeaderKeyValue_KeyNotExists_ReturnsNull()
    {
        // Arrange
        const string keyName = "name";
        const string keyValue = "value";

        var request = BuildHttpRequest();
        request.Headers.Append(keyName, keyValue);

        // Act
        var result = request.GetHeaderKeyValue("non-existent-key");

        // Assert
        Assert.Null(result);
    }

    #endregion

    #region GetRouteParameter

    /// <summary>
    /// Test for <see cref="HttpRequestExtensions.GetRouteParameter"/> method.
    /// </summary>
    [Fact]
    public void GetRouteParameter_ParamExists_ReturnsKeyValue()
    {
        // Arrange
        const string paramName = "name";
        const string paramValue = "value";

        var request = BuildHttpRequest();
        request.RouteValues.Add(paramName, paramValue);

        // Act
        var result = request.GetRouteParameter(paramName);

        // Assert
        Assert.Equal(paramValue, result);
    }

    /// <summary>
    /// Test for <see cref="HttpRequestExtensions.GetRouteParameter"/> method.
    /// </summary>
    [Fact]
    public void GetRouteParameter_ParamNotExists_ReturnsNull()
    {
        // Arrange
        const string paramName = "name";
        const string paramValue = "value";

        var request = BuildHttpRequest();
        request.RouteValues.Add(paramName, paramValue);

        // Act
        var result = request.GetRouteParameter("non-existent-param");

        // Assert
        Assert.Null(result);
    }

    #endregion

    #region GetQueryParameter

    /// <summary>
    /// Test for <see cref="HttpRequestExtensions.GetQueryParameter"/> method.
    /// </summary>
    [Fact]
    public void GetQueryParameter_ParamExists_ReturnsKeyValue()
    {
        // Arrange
        const string paramName = "name";
        const string paramValue = "value";

        var request = BuildHttpRequest();
        request.QueryString = new QueryString($"?abc=123&{paramName}={paramValue}");

        // Act
        var result = request.GetQueryParameter(paramName);

        // Assert
        Assert.Equal(paramValue, result);
    }

    /// <summary>
    /// Test for <see cref="HttpRequestExtensions.GetQueryParameter"/> method.
    /// </summary>
    [Fact]
    public void GetQueryParameter_ParamNotExists_ReturnsNull()
    {
        // Arrange
        const string paramName = "name";

        var request = BuildHttpRequest();
        request.QueryString = new QueryString("?abc=123");

        // Act
        var result = request.GetQueryParameter(paramName);

        // Assert
        Assert.Null(result);
    }

    #endregion

    #region Private methods

    private static HttpRequest BuildHttpRequest()
    {
        return new DefaultHttpContext().Request;
    }

    #endregion
}