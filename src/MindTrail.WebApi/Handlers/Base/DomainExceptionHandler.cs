using System;
using MindTrail.DomainServices.Exceptions.Base;
using MindTrail.WebApi.Abstractions.Builders;
using MindTrail.WebApi.Abstractions.Factories;
using MindTrail.WebApi.Interfaces.Handlers;

namespace MindTrail.WebApi.Handlers.Base;

public abstract class DomainExceptionHandler<TException>(IProblemDetailsBuilderFactory problemDetailsBuilderFactory)
    : IDomainExceptionHandler<DomainException>
    where TException : DomainException
{
    protected readonly IProblemDetailsBuilderFactory ProblemDetailsBuilderFactory = problemDetailsBuilderFactory;

    public bool CanHandle(DomainException e) => e is TException;

    public IProblemDetailsBuilder Handle(DomainException e, string? invalidPropName = null)
    {
        ArgumentNullException.ThrowIfNull(e);

        if (e is not TException typedEx)
        {
            throw new ArgumentException(
                $"The exception type must be {typeof(TException).Name}. " +
                $"Actual: {e.GetType().Name}.");
        }

        return Handle(typedEx, invalidPropName);
    }

    protected abstract IProblemDetailsBuilder Handle(TException e, string? invalidPropName = null);
}