using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ThoughtGuide.WebAuth.Extensions;

/// <summary>
/// HTTP request extensions.
/// </summary>
public static class HttpRequestExtensions
{
    /// <summary>
    /// Returns query parameter from HTTP request.
    /// </summary>
    /// <param name="httpRequest">HTTP request.</param>
    /// <param name="name">Parameter name.</param>
    /// <returns>The parameter value if the parameter is found; <c>null</c> otherwise.</returns>
    public static string GetQueryParameter(this HttpRequest httpRequest, string name)
    {
        if (!httpRequest.Query.TryGetValue(name, out var value)) return null;

        return value.Count != 0 ? value.First() : null;
    }

    /// <summary>
    /// Returns route parameter from HTTP request.
    /// </summary>
    /// <param name="httpRequest">HTTP request.</param>
    /// <param name="name">Parameter name.</param>
    /// <returns>The parameter value if the parameter is found; <c>null</c> otherwise.</returns>
    public static string GetRouteParameter(this HttpRequest httpRequest, string name)
    {
        return httpRequest.RouteValues.TryGetValue(name, out var value)
            ? value?.ToString()
            : null;
    }

    /// <summary>
    /// Returns the value of the header key. 
    /// </summary>
    /// <param name="httpRequest">HTTP request.</param>
    /// <param name="key">Header key.</param>
    /// <returns>The key value if the key is found in the header; <c>null</c> otherwise.</returns>
    public static string GetHeaderKeyValue(this HttpRequest httpRequest, string key)
    {
        return httpRequest.Headers.TryGetValue(key, out var value)
            ? value.FirstOrDefault()
            : null;
    }
}