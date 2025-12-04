using System;
using Microsoft.AspNetCore.Http;

namespace MindTrail.WebApi.Attributes;

/// <summary>
/// Custom attribute for controller actions, which is used to provide the description of response header.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class ResponseHeaderAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResponseHeaderAttribute"/> class.
    /// </summary>
    /// <param name="name">Response header name.</param>
    public ResponseHeaderAttribute(string name)
        : this(name, string.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResponseHeaderAttribute"/> class.
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
    /// Gets the name of the response header.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the HTTP status code.
    /// </summary>
    public int StatusCode { get; init; }

    /// <summary>
    /// Gets the type of the response header value.
    /// </summary>
    public string Type { get; init; }

    /// <summary>
    /// Gets the description of the response header.
    /// </summary>
    public string Description { get; init; }
}