using MindTrail.DomainShared.Exceptions;
using MindTrail.WebApi.Abstractions.Builders;
using MindTrail.WebApi.Abstractions.Factories;
using MindTrail.WebApi.Handlers.Base;

namespace MindTrail.WebApi.Handlers;

public sealed class BirthYearOutOfRangeExceptionHandler(
    IProblemDetailsBuilderFactory problemDetailsBuilderFactory)
    : DomainExceptionHandler<BirthYearOutOfRangeException>(problemDetailsBuilderFactory)
{
    protected override IProblemDetailsBuilder Handle(
        BirthYearOutOfRangeException e,
        string? invalidPropName = null)
    {
        return ProblemDetailsBuilderFactory.Create(e)
            .AddTitle("Birth year is outside the valid range")
            .AddParameter("minBirthYear", (int?)e.MinBirthYear)
            .AddParameter("specifiedBirthYear", (int?)e.SpecifiedBirthYear)
            .AddValidationErrorDescription(
                invalidPropName,
                $"Must be greater than {e.MinBirthYear} and earlier than the current year");
    }
}