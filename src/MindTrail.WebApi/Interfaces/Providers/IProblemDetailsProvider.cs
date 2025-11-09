using Microsoft.AspNetCore.Mvc;
using MindTrail.DomainServices.Exceptions.Base;
using MindTrail.WebApi.Builders;

namespace MindTrail.WebApi.Interfaces.Providers;

/// <summary>
/// Provides HTTP problem details responses using <see cref="ProblemDetailsBuilder"/>
/// that contain domain exceptions and error details.
/// </summary>
public interface IProblemDetailsProvider
{
    /// <summary>
    /// Creates a 400 (Bad Request) response from the provided <see cref="ProblemDetailsBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="ProblemDetailsBuilder"/> containing error details and the source <see cref="DomainException"/>.</param>
    /// <returns>An <see cref="IActionResult"/> representing the HTTP 400 (Bad Request) response.</returns>
    IActionResult CreateBadRequest(ProblemDetailsBuilder builder);

    /// <summary>
    /// Creates a 409 (Conflict) response from the provided <see cref="ProblemDetailsBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="ProblemDetailsBuilder"/> containing error details and the source <see cref="DomainException"/>.</param>
    /// <returns>An <see cref="IActionResult"/> representing the HTTP 409 (Conflict) response.</returns>
    IActionResult CreateConflict(ProblemDetailsBuilder builder);

    /// <summary>
    /// Creates a 404 ((Not Found)) response from the provided <see cref="ProblemDetailsBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="ProblemDetailsBuilder"/> containing error details and the source <see cref="DomainException"/>.</param>
    /// <returns>An <see cref="IActionResult"/> representing the HTTP 404 ((Not Found)) response.</returns>
    IActionResult CreateNotFound(ProblemDetailsBuilder builder);
}