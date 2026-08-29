using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using MindTrail.WebAuth.Constants;

namespace MindTrail.WebApi.Tests.Extensions;

/// <summary>
/// Provides extension methods for building requests and reading responses in tests.
/// </summary>
public static class HttpMessageExtensions
{
    /// <summary>
    /// Adds the API key header to the request.
    /// </summary>
    /// <param name="request">The request to add the header to.</param>
    /// <param name="apiKey">The API key value.</param>
    /// <returns>The same request instance, so that additional calls can be chained.</returns>
    public static HttpRequestMessage AddApiKey(this HttpRequestMessage request, string apiKey)
    {
        request.Headers.Add(ApiKeyConstants.ApiKeyHeaderName, apiKey);

        return request;
    }

    /// <summary>
    /// Adds a query parameter to the request URI.
    /// </summary>
    /// <param name="request">The request to add the query parameter to.</param>
    /// <param name="baseUrl">The base URL used to resolve a relative request URI.</param>
    /// <param name="paramName">The query parameter name.</param>
    /// <param name="paramValue">The query parameter value.</param>
    /// <returns>The same request instance, so that additional calls can be chained.</returns>
    public static HttpRequestMessage AddQueryParam(
        this HttpRequestMessage request, string baseUrl, string paramName, string paramValue)
    {
        var uriBuilder = GetUriBuilder(request, baseUrl);
        uriBuilder.AddQueryParam(paramName, paramValue);
        request.RequestUri = uriBuilder.Uri;

        return request;
    }

    /// <summary>
    /// Adds multiple query parameters to the request URI.
    /// </summary>
    /// <param name="request">The request to add the query parameters to.</param>
    /// <param name="baseUrl">The base URL used to resolve a relative request URI.</param>
    /// <param name="parameters">The query parameters to add.</param>
    /// <returns>The same request instance, so that additional calls can be chained.</returns>
    public static HttpRequestMessage AddQueryParams(
        this HttpRequestMessage request, string baseUrl, Dictionary<string, string> parameters)
    {
        var uriBuilder = GetUriBuilder(request, baseUrl);
        uriBuilder.AddQueryParams(parameters);
        request.RequestUri = uriBuilder.Uri;

        return request;
    }

    /// <summary>
    /// Adds a JSON string as the request content.
    /// </summary>
    /// <param name="request">The request to add the content to.</param>
    /// <param name="content">The JSON content.</param>
    /// <returns>The same request instance, so that additional calls can be chained.</returns>
    public static HttpRequestMessage AddContent(this HttpRequestMessage request, string content)
    {
        request.Content = new StringContent(content, Encoding.UTF8, "application/json");

        return request;
    }

    /// <summary>
    /// Adds the specified content to the request.
    /// </summary>
    /// <param name="request">The request to add the content to.</param>
    /// <param name="content">The content to add.</param>
    /// <returns>The same request instance, so that additional calls can be chained.</returns>
    public static HttpRequestMessage AddContent(this HttpRequestMessage request, HttpContent content)
    {
        request.Content = content;
        return request;
    }

    /// <summary>
    /// Deserializes the response content into the specified type.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the content into.</typeparam>
    /// <param name="response">The response to read the content from.</param>
    /// <returns>The deserialized content, or <c>null</c> if deserialization fails.</returns>
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

    /// <summary>
    /// Parses the response content as a <see cref="Guid"/>.
    /// </summary>
    /// <param name="response">The response to read the content from.</param>
    /// <returns>The parsed <see cref="Guid"/>.</returns>
    /// <exception cref="InvalidOperationException">The response content is not a valid <see cref="Guid"/>.</exception>
    public static async Task<Guid> GetGuidAsync(this HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();

        return Guid.TryParse(content.Trim('\"', ' ', '\r', '\n'), out var guid)
            ? guid
            : throw new InvalidOperationException(
                $"Failed to parse the Guid from the API response. Received: {content}");
    }

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