using System;
using MindTrail.DomainShared.Exceptions.Base;
using MindTrail.WebApi.Abstractions.Builders;
using MindTrail.WebApi.Abstractions.Factories;
using MindTrail.WebApi.Interfaces.Handlers;

namespace MindTrail.WebApi.Handlers.Base;

/// <inheritdoc/>
public abstract class DomainExceptionHandler<TException>(IProblemDetailsBuilderFactory problemDetailsBuilderFactory)
    : IDomainExceptionHandler<DomainException>
    where TException : DomainException
{
    /// <summary>
    /// The factory for creating <see cref="IProblemDetailsBuilder"/> instances.
    /// </summary>
    protected readonly IProblemDetailsBuilderFactory ProblemDetailsBuilderFactory = problemDetailsBuilderFactory;

    /// <inheritdoc/>
    public bool CanHandle(DomainException e) => e is TException;

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"><paramref name="e"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException"><paramref name="e"/> is not of type <typeparamref name="TException"/>.</exception>
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

    /// <summary>
    /// When overridden in a derived class,
    /// converts the specified typed domain exception into an <see cref="IProblemDetailsBuilder"/>.
    /// </summary>
    /// <param name="e">The domain exception to handle.</param>
    /// <param name="invalidPropName">The invalid property name. Ignored if <c>null</c> or empty.</param>
    /// <returns>The <see cref="IProblemDetailsBuilder"/> for the exception.</returns>
    protected abstract IProblemDetailsBuilder Handle(TException e, string? invalidPropName = null);
}