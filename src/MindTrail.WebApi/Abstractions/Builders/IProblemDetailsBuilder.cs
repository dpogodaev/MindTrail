using System;
using Microsoft.AspNetCore.Mvc;
using MindTrail.DomainServices.Exceptions.Base;

namespace MindTrail.WebApi.Abstractions.Builders;

/// <summary>
/// Fluent builder for creating <see cref="ProblemDetails"/> per RFC 9457 from <see cref="DomainException"/>.<br/>
/// Supports standard status codes 400, 404, 409.
/// </summary>
public interface IProblemDetailsBuilder
{
    DomainException Exception { get; }

    string? ErrorCode { get; }

    ProblemDetails Build(int statusCode);

    IProblemDetailsBuilder AddTitle(string title);

    IProblemDetailsBuilder AddTraceId(string? traceId);

    IProblemDetailsBuilder AddErrorCode(string? code);

    IProblemDetailsBuilder AddInstance(string? instance);

    IProblemDetailsBuilder AddValidationErrorDescription(string? invalidPropName, string? errorDescription = null);

    IProblemDetailsBuilder AddParameter(string name, string? value);

    IProblemDetailsBuilder AddParameter(string name, int? value);

    IProblemDetailsBuilder AddParameter(string name, DateTime? value);
}