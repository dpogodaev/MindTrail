using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindTrail.DomainShared.Exceptions.Base;
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

    private readonly Dictionary<string, object> _parameters = new();
    private readonly Dictionary<string, string[]> _validationErrors = new();

    private string? _title;
    private string? _detail;
    private string? _traceId;
    private string? _instance;

    /// <inheritdoc cref="IProblemDetailsBuilder.Exception"/>
    public DomainException Exception { get; } = e;

    /// <inheritdoc cref="IProblemDetailsBuilder.ErrorCode"/>
    public string? ErrorCode { get; private set; }

    /// <inheritdoc cref="IProblemDetailsBuilder.Build"/>
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

    /// <inheritdoc cref="IProblemDetailsBuilder.AddTitle"/>
    public IProblemDetailsBuilder AddTitle(string title)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title, nameof(title));

        _title = title;

        return this;
    }

    /// <inheritdoc cref="IProblemDetailsBuilder.AddDetail"/>
    public IProblemDetailsBuilder AddDetail(string? detail)
    {
        if (string.IsNullOrEmpty(detail))
        {
            return this;
        }

        _detail = detail;

        return this;
    }

    /// <inheritdoc cref="IProblemDetailsBuilder.AddTraceId"/>
    public IProblemDetailsBuilder AddTraceId(string? traceId)
    {
        if (string.IsNullOrEmpty(traceId))
        {
            return this;
        }

        _traceId = traceId;

        return this;
    }

    /// <inheritdoc cref="IProblemDetailsBuilder.AddErrorCode"/>
    public IProblemDetailsBuilder AddErrorCode(string? code)
    {
        if (string.IsNullOrEmpty(code))
        {
            return this;
        }

        ErrorCode = code;

        return this;
    }

    /// <inheritdoc cref="IProblemDetailsBuilder.AddInstance"/>
    public IProblemDetailsBuilder AddInstance(string? instance)
    {
        if (string.IsNullOrEmpty(instance))
        {
            return this;
        }

        _instance = instance;

        return this;
    }

    /// <inheritdoc cref="IProblemDetailsBuilder.AddValidationErrorDescription"/>
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

        _validationErrors[FirstCharToLowerCase(invalidPropName)] = [errorDescription.TrimEnd('.')];

        return this;
    }

    /// <inheritdoc cref="IProblemDetailsBuilder.AddParameter(string, string?)"/>
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

    /// <inheritdoc cref="IProblemDetailsBuilder.AddParameter(string, int?)"/>
    public IProblemDetailsBuilder AddParameter(string name, int? value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        if (value == null)
        {
            return this;
        }

        _parameters[name] = value.Value;

        return this;
    }

    /// <inheritdoc cref="IProblemDetailsBuilder.AddParameter(string, DateTime?)"/>
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
            _ => throw new ArgumentOutOfRangeException(nameof(statusCode), statusCode, "Supported: 400, 404, 409."),
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
        var detail = string.IsNullOrEmpty(_detail)
            ? Exception.Message
            : _detail;

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
        if (!string.IsNullOrEmpty(_title))
        {
            problemDetails.Title = _title.TrimEnd('.');
            return;
        }

        if (!string.IsNullOrEmpty(_detail))
        {
            problemDetails.Title = _detail.TrimEnd('.');
            return;
        }

        if (DefaultTitle.TryGetValue(problemDetails.Status!.Value, out var title))
        {
            problemDetails.Title = title;
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