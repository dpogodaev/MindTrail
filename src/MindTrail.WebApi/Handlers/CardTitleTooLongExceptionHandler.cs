using MindTrail.DomainShared.Exceptions.Cards;
using MindTrail.WebApi.Abstractions.Builders;
using MindTrail.WebApi.Abstractions.Factories;
using MindTrail.WebApi.Handlers.Base;

namespace MindTrail.WebApi.Handlers;

/// <summary>
/// Handles a <see cref="CardTitleTooLongException"/>.
/// </summary>
/// <param name="problemDetailsBuilderFactory">The factory for creating <see cref="IProblemDetailsBuilder"/> instances.</param>
public sealed class CardTitleTooLongExceptionHandler(
    IProblemDetailsBuilderFactory problemDetailsBuilderFactory)
    : DomainExceptionHandler<CardTitleTooLongException>(problemDetailsBuilderFactory)
{
    /// <inheritdoc/>
    protected override IProblemDetailsBuilder Handle(
        CardTitleTooLongException e,
        string? invalidPropName = null)
    {
        return ProblemDetailsBuilderFactory.Create(e)
            .AddTitle("The title is too long")
            .AddParameter("maxLength", e.MaxLength)
            .AddParameter("specifiedLength", e.SpecifiedTitleLength)
            .AddValidationErrorDescription(
                invalidPropName,
                $"The maximum length is {e.MaxLength} characters");
    }
}
