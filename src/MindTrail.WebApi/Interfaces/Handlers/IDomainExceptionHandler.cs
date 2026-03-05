using MindTrail.DomainShared.Exceptions.Base;
using MindTrail.WebApi.Abstractions.Builders;

namespace MindTrail.WebApi.Interfaces.Handlers;

public interface IDomainExceptionHandler<in TException>
    where TException : DomainException
{
    bool CanHandle(DomainException e);

    IProblemDetailsBuilder Handle(TException e, string? invalidPropName = null);
}