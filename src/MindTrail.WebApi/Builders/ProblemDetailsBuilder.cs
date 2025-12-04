using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindTrail.DomainServices.Exceptions.Base;

namespace MindTrail.WebApi.Builders;

// TODO: Refine comments.

/// <summary>
/// Builds a <see cref="ProblemDetails"/> response from a <see cref="DomainException"/>,
/// including optional parameters, validation errors, trace ID and error codes.
/// </summary>
public class ProblemDetailsBuilder(DomainException e)
{
    private readonly Dictionary<string, string> _parameters = new();
    private readonly Dictionary<string, string[]> _validationErrors = new();

    private string? _title;
    private string? _traceId;
    private string? _errorCode;

    /// <summary>
    /// Gets the provided exception.
    /// </summary>
    public DomainException Exception { get; } = e;

    /// <summary>
    /// Builds a <see cref="ProblemDetails"/> response based on the provided exception with the specified status.
    /// </summary>
    /// <param name="statusCode">The HTTP status code to assign to the problem details.</param>
    /// <returns>A <see cref="ProblemDetails"/> or <see cref="ValidationProblemDetails"/> instance that represents the response to the error.</returns>
    public ProblemDetails Build(int statusCode)
    {
        var problemDetails = statusCode == StatusCodes.Status400BadRequest && _validationErrors.Count > 0
            ? new ValidationProblemDetails(_validationErrors) { Title = _title, Detail = Exception.Message }
            : new ProblemDetails { Title = _title, Detail = Exception.Message };

        AddErrorCode(problemDetails);
        AddTraceId(problemDetails);
        AddParameters(problemDetails);
        AddStatus(problemDetails, statusCode);

        return problemDetails;
    }

    public ProblemDetailsBuilder AddTitle(string title)
    {
        _title = title;
        return this;
    }

    public ProblemDetailsBuilder AddTraceId(string? traceId)
    {
        _traceId = traceId;
        return this;
    }

    public ProblemDetailsBuilder AddErrorCode(string? code)
    {
        _errorCode = code;
        return this;
    }

    public ProblemDetailsBuilder AddValidationErrorDescription(string invalidPropName, string errorDescription)
    {
        _validationErrors[invalidPropName] = [errorDescription]; // TODO: invalid prop
        return this;
    }

    public ProblemDetailsBuilder AddParameter(string name, string value)
    {
        _parameters[name] = value;
        return this;
    }

    public ProblemDetailsBuilder AddParameter(string name, int? value)
    {
        if (value == null)
        {
            return this;
        }

        _parameters[name] = value.Value.ToString();
        return this;
    }

    /// <summary>
    /// Adds the status code and type to the error response.
    /// </summary>
    /// <param name="problemDetails">Information about an HTTP error response.</param>
    /// <param name="statusCode">The HTTP status code to add.</param>
    private static void AddStatus(ProblemDetails problemDetails, int statusCode)
    {
        problemDetails.Status = statusCode;
        problemDetails.Type = statusCode switch
        {
            StatusCodes.Status400BadRequest => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            StatusCodes.Status404NotFound => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
            StatusCodes.Status409Conflict => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
            _ => throw new ArgumentOutOfRangeException(nameof(statusCode), $"Unsupported status code: {statusCode}"),
        };
    }

    /// <summary>
    /// Adds the key-value pairs as extensions to the error response.
    /// </summary>
    /// <param name="problemDetails">Information about an HTTP error response.</param>
    private void AddParameters(ProblemDetails problemDetails)
    {
        if (_parameters.Count <= 0)
        {
            return;
        }

        foreach (var param in _parameters)
        {
            problemDetails.Extensions[param.Key] = param.Value;
        }
    }

    /// <summary>
    /// Adds the error code as extensions to the error response.
    /// </summary>
    /// <param name="problemDetails">Information about an HTTP error response.</param>
    private void AddErrorCode(ProblemDetails problemDetails)
    {
        problemDetails.Extensions["errorCode"] = _errorCode;
    }

    /// <summary>
    /// Adds the trace ID as extensions to the error response.
    /// </summary>
    /// <param name="problemDetails">Information about an HTTP error response.</param>
    private void AddTraceId(ProblemDetails problemDetails)
    {
        problemDetails.Extensions["traceId"] = _traceId;
    }
}