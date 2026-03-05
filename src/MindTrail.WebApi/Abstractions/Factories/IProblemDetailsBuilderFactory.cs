using MindTrail.DomainShared.Exceptions.Base;
using MindTrail.WebApi.Abstractions.Builders;

namespace MindTrail.WebApi.Abstractions.Factories;

public interface IProblemDetailsBuilderFactory
{
    IProblemDetailsBuilder Create(DomainException e);
}