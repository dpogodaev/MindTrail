using System;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi;

namespace MindTrail.WebApi.Attributes;

/// <summary>
/// Specifies the name, HTTP status code, and description of a response header for the annotated controller action.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class ResponseHeaderAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResponseHeaderAttribute"/> class.
    /// </summary>
    /// <param name="name">The response header name.</param>
    public ResponseHeaderAttribute(string name)
        : this(name, string.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResponseHeaderAttribute"/> class.
    /// </summary>
    /// <param name="name">The response header name.</param>
    /// <param name="description">The response header description.</param>
    public ResponseHeaderAttribute(string name, string description)
    {
        Name = name;
        Description = description;
        StatusCode = StatusCodes.Status200OK;
        Type = JsonSchemaType.String;
    }

    /// <summary>
    /// Gets the name of the response header.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the HTTP status code the response header is associated with.
    /// </summary>
    /// <remarks>
    /// Default value is <see cref="StatusCodes.Status200OK"/>.
    /// </remarks>
    public int StatusCode { get; init; }

    /// <summary>
    /// Gets the JSON Schema type of the response header value.
    /// </summary>
    /// <remarks>
    /// Default value is <see cref="JsonSchemaType.String"/>.
    /// </remarks>
    public JsonSchemaType Type { get; init; }

    /// <summary>
    /// Gets the description of the response header.
    /// </summary>
    public string Description { get; init; }
}