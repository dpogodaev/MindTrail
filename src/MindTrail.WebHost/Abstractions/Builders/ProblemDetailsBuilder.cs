using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindTrail.DomainServices.Exceptions.Base;
using MindTrail.WebApi.Abstractions.Builders;

namespace MindTrail.WebHost.Abstractions.Builders;

/// <inheritdoc cref="IProblemDetailsBuilder"/>
public class ProblemDetailsBuilder(DomainException e)
    : IProblemDetailsBuilder
{
    private const string DatetimeFormat = "yyyy-MM-ddTHH:mm:ss.fffZ"; // See "RFC 3339, section 5.6".

    private const string DefaultValidationErrorDescription = "Invalid value";

    private static readonly Dictionary<int, string> DefaultTitle = new()
    {
        [StatusCodes.Status400BadRequest] = "One or more validation errors occurred",
        [StatusCodes.Status404NotFound] = "Not Found",
        [StatusCodes.Status409Conflict] = "Conflict",
    };

    private static readonly Dictionary<int, string> TypeUri = new()
    {
        [StatusCodes.Status400BadRequest] = "https://tools.ietf.org/doc/html/rfc9110#section-15.5.1",
        [StatusCodes.Status404NotFound] = "https://tools.ietf.org/doc/html/rfc9110#section-15.5.5",
        [StatusCodes.Status409Conflict] = "https://tools.ietf.org/doc/html/rfc9110#section-15.5.10",
    };

    private readonly Dictionary<string, string> _parameters = new();
    private readonly Dictionary<string, string[]> _validationErrors = new();

    private string? _title;
    private string? _traceId;
    private string? _instance;

    /// <summary>
    /// Gets source domain exception.
    /// </summary>
    public DomainException Exception { get; } = e;

    /// <summary>
    /// Gets an application-specific error code.
    /// </summary>
    public string? ErrorCode { get; private set; }

    /// <summary>
    /// Creates final <see cref="ProblemDetails"/> with specified HTTP status code.<br/>
    /// Automatically applies all added title, trace ID, error code, parameters, and validation errors.
    /// </summary>
    /// <param name="statusCode">
    /// HTTP status code: <c>400</c> (BadRequest), <c>404</c> (NotFound), <c>409</c> (Conflict).
    /// </param>
    /// <returns>Ready <see cref="ProblemDetails"/> for HTTP response.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The specified status code is not supported.</exception>
    public ProblemDetails Build(int statusCode)
    {
        var problemDetails = CreateProblemDetails(statusCode);

        SetTitle(problemDetails);
        SetTypeUri(problemDetails);
        SetDetail(problemDetails);
        SetParametersIfAvailable(problemDetails);
        SetTraceIdIfAvailable(problemDetails);
        SetErrorCodeIfAvailable(problemDetails);
        SetInstanceIfAvailable(problemDetails);

        return problemDetails;
    }

    /// <summary>
    /// Adds a title for <see cref="ProblemDetails"/>.<br/>
    /// Default title is applied automatically based on HTTP status code.
    /// </summary>
    /// <param name="title">Short problem type description.</param>
    /// <returns>Current builder for fluent API.</returns>
    /// <exception cref="ArgumentException">The title has a <c>null</c> or empty value.</exception>
    public IProblemDetailsBuilder AddTitle(string title)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title, nameof(title));

        _title = title;

        return this;
    }

    /// <summary>
    /// Adds a trace ID to <see cref="ProblemDetails.Extensions"/> with key <c>"traceId"</c>.
    /// </summary>
    /// <param name="traceId">Trace ID. Ignored if <c>null</c> or empty.</param>
    /// <returns>Current builder for fluent API.</returns>
    public IProblemDetailsBuilder AddTraceId(string? traceId)
    {
        if (string.IsNullOrEmpty(traceId))
        {
            return this;
        }

        _traceId = traceId;

        return this;
    }

    /// <summary>
    /// Adds the application-specific error code to <see cref="ProblemDetails.Extensions"/> with key <c>"errorCode"</c>.
    /// </summary>
    /// <param name="code">Application-specific error code. Ignored if <c>null</c> or empty.</param>
    /// <returns>Current builder for fluent API.</returns>
    public IProblemDetailsBuilder AddErrorCode(string? code)
    {
        if (string.IsNullOrEmpty(code))
        {
            return this;
        }

        ErrorCode = code;

        return this;
    }

    public IProblemDetailsBuilder AddInstance(string? instance)
    {
        if (string.IsNullOrEmpty(instance))
        {
            return this;
        }

        _instance = instance;

        return this;
    }

    /// <summary>
    /// Adds validation error for property.
    /// </summary>
    /// <param name="invalidPropName">Property name. Ignored if <c>null</c> or empty.</param>
    /// <param name="errorDescription">
    /// Short message for UI. Default value is <c>"Invalid value"</c>.
    /// </param>
    /// <returns>Current builder for fluent API.</returns>
    public IProblemDetailsBuilder AddValidationErrorDescription(
        string? invalidPropName,
        string? errorDescription = null)
    {
        if (string.IsNullOrEmpty(invalidPropName))
        {
            return this;
        }

        if (string.IsNullOrEmpty(errorDescription))
        {
            errorDescription = DefaultValidationErrorDescription;
        }

        _validationErrors[FirstCharToLowerCase(invalidPropName)] = [errorDescription];

        return this;
    }

    /// <summary>
    /// Adds string parameter to extensions["parameterName"] = "value".
    /// </summary>
    /// <param name="name">Parameter name (e.g., "currentLength").</param>
    /// <param name="value">Parameter value.</param>
    /// <returns>Current builder for fluent API.</returns>
    /// <exception cref="ArgumentException">If name is null or whitespace.</exception>
    public IProblemDetailsBuilder AddParameter(string name, string? value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        if (string.IsNullOrEmpty(value))
        {
            return this;
        }

        _parameters[name] = value;

        return this;
    }

    /// <summary>
    /// Adds integer parameter to extensions["parameterName"] = 123.
    /// </summary>
    /// <param name="name">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <returns>Current builder for fluent API.</returns>
    /// <exception cref="ArgumentException">If name is null or whitespace.</exception>
    public IProblemDetailsBuilder AddParameter(string name, int? value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        if (value == null)
        {
            return this;
        }

        _parameters[name] = value.Value.ToString();

        return this;
    }

    /// <summary>
    /// Adds date in RFC 3339 format to extensions["parameterName"] = "2026-02-23T11:02:00.000Z".
    /// </summary>
    /// <param name="name">Parameter name.</param>
    /// <param name="value">Date value.</param>
    /// <returns>Current builder for fluent API.</returns>
    /// <exception cref="ArgumentException">If name is null or whitespace.</exception>
    public IProblemDetailsBuilder AddParameter(string name, DateTime? value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        if (value == null)
        {
            return this;
        }

        _parameters[name] = value.Value.ToString(DatetimeFormat);

        return this;
    }

    private ProblemDetails CreateProblemDetails(int statusCode)
    {
        return statusCode switch
        {
            StatusCodes.Status400BadRequest => CreateBadRequest(),
            StatusCodes.Status409Conflict => CreateConflict(),
            StatusCodes.Status404NotFound => CreateNotFound(),
            _ => throw new ArgumentOutOfRangeException(nameof(statusCode), statusCode, "Supported: 400, 404, 409"),
        };
    }

    private ProblemDetails CreateBadRequest()
    {
        var problemDetails = _validationErrors.Count > 0
            ? new ValidationProblemDetails(_validationErrors)
            : new ProblemDetails();

        problemDetails.Status = StatusCodes.Status400BadRequest;

        return problemDetails;
    }

    private ProblemDetails CreateConflict()
    {
        return new ProblemDetails
        {
            Status = StatusCodes.Status409Conflict,
        };
    }

    private ProblemDetails CreateNotFound()
    {
        return new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
        };
    }

    private void SetDetail(ProblemDetails problemDetails)
    {
        var detail = Exception.Message;

        if (!string.IsNullOrEmpty(detail))
        {
            problemDetails.Detail = detail;
        }
    }

    private void SetTypeUri(ProblemDetails problemDetails)
    {
        if (TypeUri.TryGetValue(problemDetails.Status!.Value, out var typeUri))
        {
            problemDetails.Type = typeUri;
        }
    }

    private void SetTitle(ProblemDetails problemDetails)
    {
        if (string.IsNullOrEmpty(_title))
        {
            SetDefaultTitle(problemDetails);
        }
        else
        {
            problemDetails.Title = _title;
        }
    }

    private void SetDefaultTitle(ProblemDetails problemDetails)
    {
        if (DefaultTitle.TryGetValue(problemDetails.Status!.Value, out var title))
        {
            problemDetails.Title = title;
        }
    }

    private void SetParametersIfAvailable(ProblemDetails problemDetails)
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

    private void SetErrorCodeIfAvailable(ProblemDetails problemDetails)
    {
        if (string.IsNullOrEmpty(ErrorCode))
        {
            return;
        }

        problemDetails.Extensions["errorCode"] = ErrorCode;
    }

    private void SetInstanceIfAvailable(ProblemDetails problemDetails)
    {
        if (string.IsNullOrEmpty(_instance))
        {
            return;
        }

        problemDetails.Instance = _instance;
    }

    private void SetTraceIdIfAvailable(ProblemDetails problemDetails)
    {
        if (string.IsNullOrEmpty(_traceId))
        {
            return;
        }

        problemDetails.Extensions["traceId"] = _traceId;
    }

    private string FirstCharToLowerCase(string source)
    {
        if (string.IsNullOrEmpty(source) || char.IsLower(source[0]))
        {
            return source;
        }

        return source.Length == 1
            ? char.ToLower(source[0]).ToString()
            : char.ToLower(source[0]) + source[1..];
    }
}