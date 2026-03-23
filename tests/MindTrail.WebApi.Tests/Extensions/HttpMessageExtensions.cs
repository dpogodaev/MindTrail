using System.Net;
using System.Text;
using System.Text.Json;
using System.Web;
using MindTrail.WebAuth.Constants;

namespace MindTrail.WebApi.Tests.Extensions;

public static class HttpMessageExtensions
{
    #region Request

    public static HttpRequestMessage AddApiKey(this HttpRequestMessage request, string apiKey)
    {
        request.Headers.Add(ApiKeyConstants.ApiKeyHeaderName, apiKey);

        return request;
    }

    public static HttpRequestMessage AddQueryParam(
        this HttpRequestMessage request, string baseUrl, string paramName, string paramValue)
    {
        var uriBuilder = GetUriBuilder(request, baseUrl);
        uriBuilder.AddQueryParam(paramName, paramValue);
        request.RequestUri = uriBuilder.Uri;

        return request;
    }

    public static HttpRequestMessage AddQueryParams(
        this HttpRequestMessage request, string baseUrl, Dictionary<string, string> parameters)
    {
        var uriBuilder = GetUriBuilder(request, baseUrl);
        uriBuilder.AddQueryParams(parameters);
        request.RequestUri = uriBuilder.Uri;

        return request;
    }

    public static HttpRequestMessage AddContent(this HttpRequestMessage request, string content)
    {
        request.Content = new StringContent(content, Encoding.UTF8, "application/json");

        return request;
    }

    public static HttpRequestMessage AddContent(this HttpRequestMessage request, HttpContent content)
    {
        request.Content = content;
        return request;
    }

    #endregion

    #region Response

    public static bool IsOk(this HttpResponseMessage response)
    {
        return response.StatusCode == HttpStatusCode.OK;
    }

    public static bool IsUnauthorized(this HttpResponseMessage response)
    {
        return response.StatusCode == HttpStatusCode.Unauthorized;
    }

    public static bool IsForbidden(this HttpResponseMessage response)
    {
        return response.StatusCode == HttpStatusCode.Forbidden;
    }

    public static bool IsConflict(this HttpResponseMessage response)
    {
        return response.StatusCode == HttpStatusCode.Conflict;
    }

    public static bool IsBadRequest(this HttpResponseMessage response)
    {
        return response.StatusCode == HttpStatusCode.BadRequest;
    }

    public static async Task<T?> GetContentAsync<T>(this HttpResponseMessage response)
        where T : class
    {
        var content = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        return JsonSerializer.Deserialize<T>(content, options);
    }

    #endregion

    private static UriBuilder GetUriBuilder(HttpRequestMessage request, string baseUrl)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "HttpRequestMessage cannot be null.");
        }

        if (request.RequestUri == null)
        {
            throw new ArgumentNullException(nameof(request), "RequestUri cannot be null.");
        }

        if (!request.RequestUri.IsAbsoluteUri)
        {
            request.RequestUri = new Uri(new Uri(baseUrl), request.RequestUri);
        }

        return new UriBuilder(request.RequestUri);
    }

    private static void AddQueryParam(this UriBuilder uriBuilder, string paramName, string paramValue)
    {
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);

        query[paramName] = paramValue;

        uriBuilder.Query = query.ToString() ?? string.Empty;
    }

    private static void AddQueryParams(this UriBuilder uriBuilder, Dictionary<string, string> parameters)
    {
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);

        foreach (var p in parameters)
        {
            query[p.Key] = p.Value;
        }

        uriBuilder.Query = query.ToString() ?? string.Empty;
    }
}