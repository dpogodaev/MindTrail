using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MindTrail.HostConfiguration.Extensions;
using MindTrail.WebApi.Controllers;
using MindTrail.WebApi.Dtos;
using MindTrail.WebAuth.Constants;
using Xunit;

namespace MindTrail.WebHost.Tests.ApiTests;

/// <summary>
/// Tests for <see cref="BuildInfoController"/> class.
/// </summary>
public class BuildInfoControllerTests(WebApplicationFactory<Program> app)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private const string AppName = "MindTrail.WebApi";
    private const string Version = "1.0.0.0";

    private readonly HttpClient _client = app.CreateClient();
    private readonly IConfiguration _configuration = app.Services.GetRequiredService<IConfiguration>();

    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    /// <summary>
    /// Test for <see cref="BuildInfoController.HeadInfo"/> method.
    /// </summary>
    [Theory]
    [InlineData("HEAD", "api/thought-guide/v1/info")]
    public async Task HeadInfo_Call_ReturnsBuildInfoInResponseHeader(string method, string url)
    {
        // Arrange
        var request = new HttpRequestMessage(new HttpMethod(method), url);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.Equal(200, (int)response.StatusCode);
        Assert.Equal(Version, GetHeaderValue(response.Headers, "X-Version"));
        Assert.Equal(AppName, GetHeaderValue(response.Headers, "X-App-Name"));
    }

    /// <summary>
    /// Test for <see cref="BuildInfoController.GetInfo"/> method.
    /// </summary>
    [Theory]
    [InlineData("GET", "api/thought-guide/v1/info")]
    public async Task GetInfo_Call_ReturnsBuildInfoInResponseBody(string method, string url)
    {
        // Arrange
        var request = new HttpRequestMessage(new HttpMethod(method), url);
        AddApiKey(request);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.Equal(200, (int)response.StatusCode);
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

        var buildInfo = await GetBuildInfo(response);
        Assert.Equal(Version, buildInfo.Version);
        Assert.Equal(AppName, buildInfo.AppName);
    }

    #region Private methods

    private async Task<BuildInfoDto> GetBuildInfo(HttpResponseMessage response)
    {
        var contentStream = await response.Content.ReadAsStreamAsync();

        return await JsonSerializer.DeserializeAsync<BuildInfoDto>(contentStream, _serializerOptions);
    }

    private static string GetHeaderValue(HttpResponseHeaders headers, string headerName)
    {
        return headers.FirstOrDefault(x => x.Key == headerName).Value?.FirstOrDefault();
    }

    private void AddApiKey(HttpRequestMessage request)
    {
        var apiKey = _configuration.GetProperty("App:ApiKey");

        request.Headers.Add(ApiKeyConstants.ApiKeyHeaderName, apiKey);
    }

    #endregion
}