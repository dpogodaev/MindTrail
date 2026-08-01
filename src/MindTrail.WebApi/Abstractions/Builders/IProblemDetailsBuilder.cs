using System;
using Microsoft.AspNetCore.Mvc;
using MindTrail.DomainShared.Exceptions.Base;

namespace MindTrail.WebApi.Abstractions.Builders;

/// <summary>
/// Fluent builder for creating <see cref="ProblemDetails"/> per RFC 9457 from <see cref="DomainException"/>.
/// Supports standard status codes 400, 404, and 409.
/// </summary>
public interface IProblemDetailsBuilder
{
    /// <summary>
    /// Gets the source domain exception.
    /// </summary>
    DomainException Exception { get; }

    /// <summary>
    /// Gets an application-specific error code.
    /// </summary>
    string? ErrorCode { get; }

    /// <summary>
    /// Creates the final <see cref="ProblemDetails"/> with the specified HTTP status code.<br/>
    /// Automatically applies all added title, detail, trace ID, error code, parameters, and validation errors.
    /// </summary>
    /// <param name="statusCode">
    /// HTTP status code: <c>400 (Bad Request)</c>, <c>404 (Not Found)</c>, <c>409 (Conflict)</c>.
    /// </param>
    /// <returns>The ready <see cref="ProblemDetails"/> for the HTTP response.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The specified status code is not supported.</exception>
    ProblemDetails Build(int statusCode);

    /// <summary>
    /// Adds a title for the <see cref="ProblemDetails"/>.<br/>
    /// Uses detail message if empty; otherwise applies default title based on HTTP status code.
    /// </summary>
    /// <param name="title">The short problem type description.</param>
    /// <returns>The current builder instance for fluent API.</returns>
    /// <exception cref="ArgumentException">The <paramref name="title"/> has a <c>null</c> or empty value.</exception>
    IProblemDetailsBuilder AddTitle(string title);

    /// <summary>
    /// Adds a detail message for the <see cref="ProblemDetails"/>.<br/>
    /// If <paramref name="detail"/> is <c>null</c> or empty, uses <see cref="Exception.Message"/> instead.
    /// </summary>
    /// <param name="detail">The detail message.</param>
    /// <returns>The current builder instance for fluent API.</returns>
    IProblemDetailsBuilder AddDetail(string? detail);

    /// <summary>
    /// Adds a trace ID to <see cref="ProblemDetails.Extensions"/> with key <c>"traceId"</c>.
    /// </summary>
    /// <param name="traceId">The trace ID. Ignored if <c>null</c> or empty.</param>
    /// <returns>The current builder instance for fluent API.</returns>
    IProblemDetailsBuilder AddTraceId(string? traceId);

    /// <summary>
    /// Adds an instance URI to the <see cref="ProblemDetails"/>.
    /// </summary>
    /// <param name="instance">The instance URI. Ignored if <c>null</c> or empty.</param>
    /// <returns>The current builder instance for fluent API.</returns>
    IProblemDetailsBuilder AddInstance(string? instance);

    /// <summary>
    /// Adds the application-specific error code to <see cref="ProblemDetails.Extensions"/> with key <c>"errorCode"</c>.
    /// </summary>
    /// <param name="code">Application-specific error code. Ignored if <c>null</c> or empty.</param>
    /// <returns>The current builder instance for fluent API.</returns>
    IProblemDetailsBuilder AddErrorCode(string? code);

    /// <summary>
    /// Adds a validation error description for the specified property.
    /// </summary>
    /// <param name="invalidPropName">The invalid property name. Ignored if <c>null</c> or empty.</param>
    /// <param name="errorDescription">
    /// The short description of the validation error. If <c>null</c> or empty, a default message is used.
    /// </param>
    /// <returns>The current builder instance for fluent API.</returns>
    IProblemDetailsBuilder AddValidationErrorDescription(string? invalidPropName, string? errorDescription = null);

    /// <summary>
    /// Adds a string parameter to <see cref="ProblemDetails.Extensions"/>.
    /// </summary>
    /// <param name="name">The parameter name (e.g., <c>"maxLength"</c>).</param>
    /// <param name="value">The parameter value.</param>
    /// <returns>The current builder instance for fluent API.</returns>
    /// <exception cref="ArgumentException">The <paramref name="name"/> is <c>null</c> or whitespace.</exception>
    IProblemDetailsBuilder AddParameter(string name, string? value);

    /// <summary>
    /// Adds an integer parameter to <see cref="ProblemDetails.Extensions"/>.
    /// </summary>
    /// <param name="name">The parameter name (e.g., <c>"maxLength"</c>).</param>
    /// <param name="value">The parameter value.</param>
    /// <returns>The current builder instance for fluent API.</returns>
    /// <exception cref="ArgumentException">The <paramref name="name"/> is <c>null</c> or whitespace.</exception>
    IProblemDetailsBuilder AddParameter(string name, int? value);

    /// <summary>
    /// Adds a date parameter in RFC 3339 format to <see cref="ProblemDetails.Extensions"/>.
    /// </summary>
    /// <param name="name">The parameter name (e.g., <c>"minDate"</c>).</param>
    /// <param name="value">The parameter value.</param>
    /// <returns>The current builder instance for fluent API.</returns>
    /// <exception cref="ArgumentException">The <paramref name="name"/> is <c>null</c> or whitespace.</exception>
    IProblemDetailsBuilder AddParameter(string name, DateTime? value);
}