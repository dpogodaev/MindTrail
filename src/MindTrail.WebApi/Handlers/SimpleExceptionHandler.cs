using MindTrail.DomainShared.Exceptions;
using MindTrail.WebApi.Abstractions.Builders;
using MindTrail.WebApi.Abstractions.Factories;
using MindTrail.WebApi.Handlers.Base;

namespace MindTrail.WebApi.Handlers;

public sealed class SimpleExceptionHandler(
    IProblemDetailsBuilderFactory problemDetailsBuilderFactory)
    : DomainExceptionHandler<SimpleDomainException>(problemDetailsBuilderFactory)
{
    protected override IProblemDetailsBuilder Handle(
        SimpleDomainException e,
        string? invalidPropName = null)
    {
        return ProblemDetailsBuilderFactory.Create(e)
            .AddValidationErrorDescription(invalidPropName);
    }
}