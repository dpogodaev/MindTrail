using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace ThoughtGuide.WebApi.Handlers;

/// <summary>
/// Adds custom exception handling that produces a <see cref="ProblemDetails"/> response for all unhandled exceptions.
/// </summary>
/// <param name="environment">Information about the hosting environment.</param>
/// <remarks>
/// The <see cref="ProblemDetails.Status"/> is always set to <see cref="StatusCodes.Status500InternalServerError"/>.<br/>
/// The <see cref="ProblemDetails.Title"/> contains brief information about the error.<br/>
/// The <see cref="ProblemDetails.Detail"/> contains detailed information about the exception and provided only for the development environment.<br/>
/// The <see cref="ProblemDetails.Extensions"/> contains the trace ID.
/// </remarks>
public sealed class CustomExceptionHandler(IHostEnvironment environment) : IExceptionHandler
{
    /// <inheritdoc cref="IExceptionHandler.TryHandleAsync"/>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var withDetailedInfo = environment.IsDevelopment();
        var problemDetails = new ProblemDetails
        {
            Type = exception.GetType().Name,
            Status = StatusCodes.Status500InternalServerError,
            Title = withDetailedInfo ? $"An error occured: {exception.Message}" : "An error occured",
            Detail = withDetailedInfo ? exception.ToString() : null,
            Extensions = { ["traceId"] = Activity.Current?.Id ?? httpContext.TraceIdentifier }
        };

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}