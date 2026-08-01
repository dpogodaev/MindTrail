using Microsoft.AspNetCore.Mvc;
using MindTrail.DomainShared.Exceptions;
using MindTrail.DomainShared.Exceptions.Base;

namespace MindTrail.WebApi.Abstractions.Providers;

/// <summary>
/// Converts <see cref="DomainException"/> instances into HTTP error results.
/// </summary>
public interface IHttpErrorResultProvider
{
    /// <summary>
    /// Creates a <c>409 Conflict</c> result from the specified domain exception.
    /// </summary>
    /// <param name="e">The source domain exception.</param>
    /// <returns>The <see cref="ConflictObjectResult"/> for the HTTP response.</returns>
    ConflictObjectResult ToConflict(DomainException e);

    /// <summary>
    /// Creates a <c>400 Bad Request</c> result from the specified domain exception.
    /// </summary>
    /// <param name="e">The source domain exception.</param>
    /// <param name="invalidPropName">The name of the invalid property.</param>
    /// <returns>The <see cref="BadRequestObjectResult"/> for the HTTP response.</returns>
    BadRequestObjectResult ToBadRequest(DomainException e, string invalidPropName);

    /// <summary>
    /// Creates a <c>400 (Bad Request)</c> result for the specified invalid property,
    /// without an underlying domain exception.
    /// </summary>
    /// <param name="invalidPropName">The name of the invalid property.</param>
    /// <param name="errorDescription">
    /// The short description of the validation error to include in the response. If <c>null</c> or empty, a default message is used.
    /// </param>
    /// <returns>The <see cref="BadRequestObjectResult"/> for the HTTP response.</returns>
    BadRequestObjectResult ToBadRequest(string invalidPropName, string? errorDescription = null);

    /// <summary>
    /// Creates a <c>404 Not Found</c> result, optionally based on the specified domain exception.
    /// </summary>
    /// <param name="e">
    /// The source domain exception. If <c>null</c>, a <see cref="SimpleDomainException"/> is used instead.
    /// </param>
    /// <returns>The <see cref="NotFoundObjectResult"/> for the HTTP response.</returns>
    NotFoundObjectResult ToNotFound(DomainException? e = null);
}