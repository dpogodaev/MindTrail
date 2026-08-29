using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MindTrail.Common.Extensions;
using MindTrail.DomainShared.Exceptions;
using MindTrail.DomainShared.Exceptions.Base;
using MindTrail.WebApi.Abstractions.Builders;
using MindTrail.WebApi.Abstractions.Factories;
using MindTrail.WebApi.Abstractions.Providers;
using MindTrail.WebApi.Handlers;
using MindTrail.WebApi.Interfaces.Handlers;

namespace MindTrail.WebHost.Abstractions.Providers;

/// <inheritdoc/>
/// <param name="logger">The logger.</param>
/// <param name="traceIdProvider">The provider of the current request's trace ID.</param>
/// <param name="errorCodeProvider">The provider of application-specific error codes for domain exceptions.</param>
/// <param name="instanceProvider">The provider of the <see cref="ProblemDetails"/> instance URI.</param>
/// <param name="problemDetailsBuilderFactory">The factory for creating <see cref="IProblemDetailsBuilder"/> instances.</param>
/// <param name="exceptionHandlers">The registered handlers for converting domain exceptions to <see cref="IProblemDetailsBuilder"/>.</param>
public class HttpErrorResultProvider(
    ILogger<HttpErrorResultProvider> logger,
    TraceIdProvider traceIdProvider,
    ErrorCodeProvider errorCodeProvider,
    ProblemInstanceProvider instanceProvider,
    IProblemDetailsBuilderFactory problemDetailsBuilderFactory,
    IEnumerable<IDomainExceptionHandler<DomainException>> exceptionHandlers)
    : IHttpErrorResultProvider
{
    /// <inheritdoc/>
    public NotFoundObjectResult ToNotFound(DomainException? e)
    {
        var problemDetailsBuilder = problemDetailsBuilderFactory
            .Create(e ?? BuildSimpleDomainException());

        var problemDetails = problemDetailsBuilder
            .AddTraceId(traceIdProvider.TraceId)
            .AddInstance(instanceProvider.GetInstance(traceIdProvider.TraceId))
            .Build(StatusCodes.Status404NotFound);

        logger.LogWarning(
            "{Title} {Details}",
            problemDetails.Title, problemDetails.Serialize());

        return new NotFoundObjectResult(problemDetails);
    }

    /// <inheritdoc/>
    public BadRequestObjectResult ToBadRequest(string invalidPropertyName, string? errorDescription = null)
    {
        var problemDetailsBuilder = problemDetailsBuilderFactory
            .Create(BuildSimpleDomainException());

        var problemDetails = problemDetailsBuilder
            .AddValidationErrorDescription(invalidPropertyName, errorDescription)
            .AddTraceId(traceIdProvider.TraceId)
            .AddInstance(instanceProvider.GetInstance(traceIdProvider.TraceId))
            .Build(StatusCodes.Status400BadRequest);

        logger.LogWarning(
            "{Title} {ErrorCode} {Details}",
            problemDetails.Title, problemDetailsBuilder.ErrorCode, problemDetails.Serialize());

        return new BadRequestObjectResult(problemDetails);
    }

    /// <inheritdoc/>
    /// <exception cref="InvalidOperationException">The <see cref="SimpleExceptionHandler"/> is not registered.</exception>
    public BadRequestObjectResult ToBadRequest(DomainException e, string? invalidPropertyName = null)
    {
        var (handler, ex) = GetMatchingHandler(e);
        var problemDetailsBuilder = handler.Handle(ex, invalidPropertyName);

        var problemDetails = problemDetailsBuilder
            .AddTraceId(traceIdProvider.TraceId)
            .AddErrorCode(errorCodeProvider.GetCode(problemDetailsBuilder.Exception))
            .AddInstance(instanceProvider.GetInstance(traceIdProvider.TraceId))
            .Build(StatusCodes.Status400BadRequest);

        logger.LogWarning(
            "{Title} {ErrorCode} {Details}",
            problemDetails.Title, problemDetailsBuilder.ErrorCode, problemDetails.Serialize());

        return new BadRequestObjectResult(problemDetails);
    }

    /// <inheritdoc/>
    /// <exception cref="InvalidOperationException">The <see cref="SimpleExceptionHandler"/> is not registered.</exception>
    public ConflictObjectResult ToConflict(DomainException e)
    {
        var (handler, ex) = GetMatchingHandler(e);
        var problemDetailsBuilder = handler.Handle(ex);

        var problemDetails = problemDetailsBuilder
            .AddTraceId(traceIdProvider.TraceId)
            .AddErrorCode(errorCodeProvider.GetCode(problemDetailsBuilder.Exception))
            .AddInstance(instanceProvider.GetInstance(traceIdProvider.TraceId))
            .Build(StatusCodes.Status409Conflict);

        logger.LogWarning(
            "{Title} {ErrorCode} {Details}",
            problemDetails.Title, problemDetailsBuilder.ErrorCode, problemDetails.Serialize());

        return new ConflictObjectResult(problemDetails);
    }

    private static SimpleDomainException BuildSimpleDomainException(DomainException? e = null)
    {
        return e == null
            ? new SimpleDomainException()
            : new SimpleDomainException(e.Message, e);
    }

    private (IDomainExceptionHandler<DomainException> Handler, DomainException ExceptionToHadle)
        GetMatchingHandler(DomainException e)
    {
        var matchingHandler = exceptionHandlers.SingleOrDefault(x => x.CanHandle(e));
        if (matchingHandler != null)
        {
            return (matchingHandler, e);
        }

        var simpleDomainException = BuildSimpleDomainException(e);
        var simpleExceptionHandler = exceptionHandlers.SingleOrDefault(x => x.CanHandle(simpleDomainException));

        return simpleExceptionHandler == null
            ? throw new InvalidOperationException($"The {nameof(SimpleExceptionHandler)} is not registered.")
            : (simpleExceptionHandler, simpleDomainException);
    }
}