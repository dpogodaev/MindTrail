using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.WebApi.Controllers;
using MindTrail.WebApi.Dtos;
using MindTrail.WebApi.Tests.Extensions;
using MindTrail.WebApi.Tests.Factories;
using MindTrail.WebApi.Tests.Providers;

namespace MindTrail.WebApi.Tests.ApiTests;

/// <summary>
/// Tests for <see cref="BuildInfoController"/> class.
/// </summary>
[TestClass]
[DoNotParallelize]
[TestCategory("API")]
public class BuildInfoControllerTests
{
    private const string ExpectedAppName = "MindTrail.WebApi";
    private const string ExpectedVersion = "1.0.0.0";

    private static CustomWebAppFactory<Program>? _appFactory;

    private BuildInfoApiProvider? _buildInfoApiProvider;

    [ClassInitialize]
    public static void Initialize(TestContext context)
    {
        _appFactory = new CustomWebAppFactory<Program>();
    }

    [TestInitialize]
    public void TestInitialize()
    {
        var client = _appFactory!.CreateClient(new WebApplicationFactoryClientOptions());

        _buildInfoApiProvider = new BuildInfoApiProvider(client);
    }

    /// <summary>
    /// Test for <see cref="BuildInfoController.HeadInfo"/> method.
    /// </summary>
    [TestMethod]
    public async Task Build_info_returned_in_response_headers()
    {
        // Act
        var response = await _buildInfoApiProvider!.HeadInfoAsync();

        // Assert
        Assert.AreEqual(200, (int)response.StatusCode);
        Assert.AreEqual(ExpectedVersion, GetHeaderValue(response.Headers, "X-Version"));
        Assert.AreEqual(ExpectedAppName, GetHeaderValue(response.Headers, "X-App-Name"));
    }

    /// <summary>
    /// Test for <see cref="BuildInfoController.GetInfo"/> method.
    /// </summary>
    [TestMethod]
    public async Task Build_info_returned_in_json_response_body()
    {
        // Act
        var response = await _buildInfoApiProvider!.GetInfoAsync();

        // Assert
        Assert.AreEqual(200, (int)response.StatusCode);
        Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

        var buildInfo = await response.GetContentAsync<BuildInfoDto>();
        Assert.AreEqual(ExpectedVersion, buildInfo?.Version);
        Assert.AreEqual(ExpectedAppName, buildInfo?.AppName);
    }

    private static string? GetHeaderValue(HttpResponseHeaders headers, string headerName)
    {
        return headers.FirstOrDefault(x => x.Key == headerName).Value?.FirstOrDefault();
    }
}