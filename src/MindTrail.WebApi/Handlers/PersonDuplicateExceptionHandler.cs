using MindTrail.DomainShared.Exceptions;
using MindTrail.WebApi.Abstractions.Builders;
using MindTrail.WebApi.Abstractions.Factories;
using MindTrail.WebApi.Handlers.Base;

namespace MindTrail.WebApi.Handlers;

/// <summary>
/// Handles a <see cref="PersonDuplicateException"/>.
/// </summary>
/// <param name="problemDetailsBuilderFactory">The factory for creating <see cref="IProblemDetailsBuilder"/> instances.</param>
public sealed class PersonDuplicateExceptionHandler(
    IProblemDetailsBuilderFactory problemDetailsBuilderFactory)
    : DomainExceptionHandler<PersonDuplicateException>(problemDetailsBuilderFactory)
{
    /// <inheritdoc/>
    protected override IProblemDetailsBuilder Handle(
        PersonDuplicateException e,
        string? invalidPropName = null)
    {
        return ProblemDetailsBuilderFactory.Create(e)
            .AddTitle("Duplicate person")
            .AddParameter("personId", e.PersonId.ToString())
            .AddParameter("specifiedFullName", e.SpecifiedFullName)
            .AddParameter("specifiedBirthYear", e.SpecifiedBirthYear);
    }
}