using MindTrail.DomainShared.Exceptions;
using MindTrail.WebApi.Abstractions.Builders;
using MindTrail.WebApi.Abstractions.Factories;
using MindTrail.WebApi.Handlers.Base;

namespace MindTrail.WebApi.Handlers;

/// <summary>
/// Handles a <see cref="SimpleDomainException"/>.
/// </summary>
/// <param name="problemDetailsBuilderFactory">The factory for creating <see cref="IProblemDetailsBuilder"/> instances.</param>
public sealed class SimpleExceptionHandler(
    IProblemDetailsBuilderFactory problemDetailsBuilderFactory)
    : DomainExceptionHandler<SimpleDomainException>(problemDetailsBuilderFactory)
{
    /// <inheritdoc/>
    protected override IProblemDetailsBuilder Handle(
        SimpleDomainException e,
        string? invalidPropName = null)
    {
        return ProblemDetailsBuilderFactory.Create(e)
            .AddValidationErrorDescription(invalidPropName);
    }
}