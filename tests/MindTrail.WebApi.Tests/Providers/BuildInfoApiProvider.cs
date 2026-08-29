using System.Net.Http;
using System.Threading.Tasks;
using MindTrail.WebApi.Controllers;

namespace MindTrail.WebApi.Tests.Providers;

/// <summary>
/// Sends requests to the <see cref="BuildInfoController"/> endpoints.
/// </summary>
/// <param name="client">The HTTP client used to send requests.</param>
public class BuildInfoApiProvider(HttpClient client)
{
    private const string BaseUrl = "api/mind-trail/v1/info";

    /// <summary>
    /// Request for endpoint <see cref="BuildInfoController.GetInfo"/>.
    /// </summary>
    /// <returns>The HTTP response message received from the endpoint.</returns>
    public async Task<HttpResponseMessage> GetInfoAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}");

        return await client.SendAsync(request);
    }

    /// <summary>
    /// Request for endpoint <see cref="BuildInfoController.HeadInfo"/>.
    /// </summary>
    /// <returns>The HTTP response message received from the endpoint.</returns>
    public async Task<HttpResponseMessage> HeadInfoAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Head, $"{BaseUrl}");

        return await client.SendAsync(request);
    }
}