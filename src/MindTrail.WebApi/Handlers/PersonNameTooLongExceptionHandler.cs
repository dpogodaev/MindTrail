using MindTrail.DomainShared.Exceptions;
using MindTrail.WebApi.Abstractions.Builders;
using MindTrail.WebApi.Abstractions.Factories;
using MindTrail.WebApi.Handlers.Base;

namespace MindTrail.WebApi.Handlers;

public sealed class PersonNameTooLongExceptionHandler(
    IProblemDetailsBuilderFactory problemDetailsBuilderFactory)
    : DomainExceptionHandler<PersonNameTooLongException>(problemDetailsBuilderFactory)
{
    protected override IProblemDetailsBuilder Handle(
        PersonNameTooLongException e,
        string? invalidPropName = null)
    {
        return ProblemDetailsBuilderFactory.Create(e)
            .AddTitle("The name is too long")
            .AddParameter("maxLength", e.MaxLength)
            .AddParameter("specifiedNameLength", e.SpecifiedNameLength)
            .AddValidationErrorDescription(
                invalidPropName,
                $"The maximum length is {e.MaxLength} characters");
    }
}