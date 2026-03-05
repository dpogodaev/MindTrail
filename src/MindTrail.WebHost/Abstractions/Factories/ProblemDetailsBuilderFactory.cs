using MindTrail.DomainShared.Exceptions.Base;
using MindTrail.WebApi.Abstractions.Builders;
using MindTrail.WebApi.Abstractions.Factories;
using MindTrail.WebHost.Abstractions.Builders;

namespace MindTrail.WebHost.Abstractions.Factories;

public class ProblemDetailsBuilderFactory : IProblemDetailsBuilderFactory
{
    public IProblemDetailsBuilder Create(DomainException e)
    {
        return new ProblemDetailsBuilder(e);
    }
}