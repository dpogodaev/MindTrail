using MindTrail.DomainShared.Exceptions.Cards;
using MindTrail.WebApi.Abstractions.Builders;
using MindTrail.WebApi.Abstractions.Factories;
using MindTrail.WebApi.Handlers.Base;

namespace MindTrail.WebApi.Handlers;

/// <summary>
/// Handles a <see cref="CardContentTooLongException"/>.
/// </summary>
/// <param name="problemDetailsBuilderFactory">The factory for creating <see cref="IProblemDetailsBuilder"/> instances.</param>
public sealed class CardContentTooLongExceptionHandler(
    IProblemDetailsBuilderFactory problemDetailsBuilderFactory)
    : DomainExceptionHandler<CardContentTooLongException>(problemDetailsBuilderFactory)
{
    /// <inheritdoc/>
    protected override IProblemDetailsBuilder Handle(
        CardContentTooLongException e,
        string? invalidPropName = null)
    {
        return ProblemDetailsBuilderFactory.Create(e)
            .AddTitle("The content is too long")
            .AddParameter("maxLength", e.MaxLength)
            .AddParameter("specifiedLength", e.SpecifiedContentLength)
            .AddValidationErrorDescription(
                invalidPropName,
                $"The maximum length is {e.MaxLength} characters");
    }
}
