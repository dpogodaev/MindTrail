using Microsoft.AspNetCore.HttpLogging;

namespace MindTrail.WebHost.Settings;

/// <summary>
/// Settings of HTTP logging.
/// </summary>
internal class HttpLoggingSettings
{
    /// <summary>
    /// Whether the HTTP request header should be logged (<see cref="HttpLoggingFields.RequestHeaders"/>).
    /// </summary>
    public bool? RequestHeaders { get; init; }

    /// <summary>
    /// Whether the HTTP request protocol should be logged (<see cref="HttpLoggingFields.RequestProtocol"/>).
    /// </summary>
    public bool? RequestProtocol { get; init; }

    /// <summary>
    /// Whether the HTTP request scheme should be logged (<see cref="HttpLoggingFields.RequestScheme"/>).
    /// </summary>
    public bool? RequestScheme { get; init; }

    /// <summary>
    /// Whether the HTTP request method should be logged (<see cref="HttpLoggingFields.RequestMethod"/>).
    /// </summary>
    public bool? RequestMethod { get; init; }

    /// <summary>
    /// Whether the HTTP request path should be logged (<see cref="HttpLoggingFields.RequestPath"/>).
    /// </summary>
    public bool? RequestPath { get; init; }

    /// <summary>
    /// Whether the HTTP request query should be logged (<see cref="HttpLoggingFields.RequestQuery"/>).
    /// </summary>
    public bool? RequestQuery { get; init; }

    /// <summary>
    /// Whether the HTTP request body should be logged (<see cref="HttpLoggingFields.RequestBody"/>).
    /// </summary>
    public bool? RequestBody { get; init; }

    /// <summary>
    /// Whether the HTTP response headers should be logged (<see cref="HttpLoggingFields.ResponseHeaders"/>).
    /// </summary>
    public bool? ResponseHeaders { get; init; }

    /// <summary>
    /// Whether the HTTP response status code should be logged (<see cref="HttpLoggingFields.ResponseStatusCode"/>).
    /// </summary>
    public bool? ResponseStatusCode { get; init; }

    /// <summary>
    /// Whether the HTTP response body should be logged (<see cref="HttpLoggingFields.ResponseBody"/>).
    /// </summary>
    public bool? ResponseBody { get; init; }
}