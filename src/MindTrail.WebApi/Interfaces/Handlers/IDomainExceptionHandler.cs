using MindTrail.DomainShared.Exceptions.Base;
using MindTrail.WebApi.Abstractions.Builders;

namespace MindTrail.WebApi.Interfaces.Handlers;

/// <summary>
/// Converts a specific <see cref="DomainException"/> subtype into an <see cref="IProblemDetailsBuilder"/>.
/// </summary>
/// <typeparam name="TException">The type of domain exception this handler can handle.</typeparam>
public interface IDomainExceptionHandler<in TException>
    where TException : DomainException
{
    /// <summary>
    /// Determines whether the specified domain exception can be handled by this handler.
    /// </summary>
    /// <param name="e">The domain exception to check.</param>
    /// <returns><c>true</c> if this handler can handle <paramref name="e"/>; otherwise, <c>false</c>.</returns>
    bool CanHandle(DomainException e);

    /// <summary>
    /// Converts the specified domain exception into an <see cref="IProblemDetailsBuilder"/>.
    /// </summary>
    /// <param name="e">The domain exception to handle.</param>
    /// <param name="invalidPropName">The invalid property name. Ignored if <c>null</c> or empty.</param>
    /// <returns>The <see cref="IProblemDetailsBuilder"/> for the exception.</returns>
    IProblemDetailsBuilder Handle(TException e, string? invalidPropName = null);
}