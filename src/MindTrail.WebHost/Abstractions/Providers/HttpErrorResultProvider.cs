using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MindTrail.Common.Extensions;
using MindTrail.DomainShared.Exceptions;
using MindTrail.DomainShared.Exceptions.Base;
using MindTrail.WebApi.Abstractions.Factories;
using MindTrail.WebApi.Abstractions.Providers;
using MindTrail.WebApi.Handlers;
using MindTrail.WebApi.Interfaces.Handlers;

namespace MindTrail.WebHost.Abstractions.Providers;

public class HttpErrorResultProvider(
    ILogger<HttpErrorResultProvider> logger,
    TraceIdProvider traceIdProvider,
    ErrorCodeProvider errorCodeProvider,
    ProblemInstanceProvider instanceProvider,
    IProblemDetailsBuilderFactory problemDetailsBuilderFactory,
    IEnumerable<IDomainExceptionHandler<DomainException>> exceptionHandlers)
    : IHttpErrorResultProvider
{
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
            ? throw new InvalidOperationException($"The {nameof(SimpleExceptionHandler)} is not registered")
            : (simpleExceptionHandler, simpleDomainException);
    }

    private SimpleDomainException BuildSimpleDomainException(DomainException? e = null)
    {
        return e == null
            ? new SimpleDomainException()
            : new SimpleDomainException(e.Message, e);
    }
}