using MindTrail.DomainShared.Exceptions;
using MindTrail.WebApi.Abstractions.Builders;
using MindTrail.WebApi.Abstractions.Factories;
using MindTrail.WebApi.Handlers.Base;

namespace MindTrail.WebApi.Handlers;

/// <summary>
/// Handles a <see cref="BirthYearOutOfRangeException"/>.
/// </summary>
/// <param name="problemDetailsBuilderFactory">The factory for creating <see cref="IProblemDetailsBuilder"/> instances.</param>
public sealed class BirthYearOutOfRangeExceptionHandler(
    IProblemDetailsBuilderFactory problemDetailsBuilderFactory)
    : DomainExceptionHandler<BirthYearOutOfRangeException>(problemDetailsBuilderFactory)
{
    /// <inheritdoc/>
    protected override IProblemDetailsBuilder Handle(
        BirthYearOutOfRangeException e,
        string? invalidPropName = null)
    {
        return ProblemDetailsBuilderFactory.Create(e)
            .AddTitle("Birth year is outside the valid range")
            .AddParameter("minBirthYear", e.MinBirthYear)
            .AddParameter("specifiedBirthYear", e.SpecifiedBirthYear)
            .AddValidationErrorDescription(
                invalidPropName,
                $"Must be greater than {e.MinBirthYear} and earlier than the current year");
    }
}