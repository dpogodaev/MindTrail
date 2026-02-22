using MindTrail.DomainServices.Exceptions;
using MindTrail.WebApi.Abstractions.Builders;
using MindTrail.WebApi.Abstractions.Factories;
using MindTrail.WebApi.Handlers.Base;

namespace MindTrail.WebApi.Handlers;

public class PersonDuplicateExceptionHandler(
    IProblemDetailsBuilderFactory problemDetailsBuilderFactory)
    : DomainExceptionHandler<PersonDuplicateException>(problemDetailsBuilderFactory)
{
    protected override IProblemDetailsBuilder Handle(
        PersonDuplicateException e,
        string? invalidPropName = null)
    {
        return ProblemDetailsBuilderFactory.Create(e)
            .AddTitle("Duplicate person")
            .AddParameter("fullName", e.FullName)
            .AddParameter("birthYear", e.BirthYear);
    }
}