using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindTrail.WebApi.Builders;
using MindTrail.WebApi.Interfaces.Providers;

namespace MindTrail.WebApi.Providers;

/// <inheritdoc/>
public class ProblemDetailsProvider(
    IErrorCodeProvider errorCodeProvider,
    ITraceIdProvider traceIdProvider)
    : IProblemDetailsProvider
{
    /// <inheritdoc cref="IProblemDetailsProvider.CreateBadRequest"/>
    public IActionResult CreateBadRequest(ProblemDetailsBuilder builder)
    {
        return new BadRequestObjectResult(builder
            .AddTraceId(traceIdProvider.TraceId)
            .AddErrorCode(GetErrorCode(builder))
            .Build(StatusCodes.Status400BadRequest));
    }

    /// <inheritdoc cref="IProblemDetailsProvider.CreateConflict"/>
    public IActionResult CreateConflict(ProblemDetailsBuilder builder)
    {
        return new ConflictObjectResult(builder
            .AddTraceId(traceIdProvider.TraceId)
            .AddErrorCode(GetErrorCode(builder))
            .Build(StatusCodes.Status409Conflict));
    }

    /// <inheritdoc cref="IProblemDetailsProvider.CreateNotFound"/>
    public IActionResult CreateNotFound(ProblemDetailsBuilder builder)
    {
        return new NotFoundObjectResult(builder
            .AddTraceId(traceIdProvider.TraceId)
            .AddErrorCode(GetErrorCode(builder))
            .Build(StatusCodes.Status404NotFound));
    }

    private string? GetErrorCode(ProblemDetailsBuilder builder)
    {
        return errorCodeProvider.TryGetCode(builder.Exception, out var code) ? code : null;
    }
}