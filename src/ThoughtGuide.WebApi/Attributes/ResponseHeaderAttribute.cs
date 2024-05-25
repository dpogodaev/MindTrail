using System;
using Microsoft.AspNetCore.Http;

namespace ThoughtGuide.WebApi.Attributes;

/// <summary>
/// Custom attribute for controller actions, which is used to provide the description of response header.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class ResponseHeaderAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of <see cref="ResponseHeaderAttribute"/> class.
    /// </summary>
    /// <param name="name">Response header name.</param>
    public ResponseHeaderAttribute(string name) : this(name, string.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ResponseHeaderAttribute"/> class.
    /// </summary>
    /// <param name="name">Response header name.</param>
    /// <param name="description">Response header description.</param>
    public ResponseHeaderAttribute(string name, string description)
    {
        Name = name;
        Description = description;
        StatusCode = StatusCodes.Status200OK;
        Type = "String";
    }

    /// <summary>
    /// Response header name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// HTTP status code.
    /// </summary>
    public int StatusCode { get; init; }

    /// <summary>
    /// Type of the response header value.
    /// </summary>
    public string Type { get; init; }

    /// <summary>
    /// Response header description.
    /// </summary>
    public string Description { get; init; }
}