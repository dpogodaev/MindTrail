using MindTrail.DomainShared.Exceptions.Base;
using MindTrail.WebApi.Abstractions.Builders;

namespace MindTrail.WebApi.Abstractions.Factories;

/// <summary>
/// Creates <see cref="IProblemDetailsBuilder"/> instances for a given <see cref="DomainException"/>.
/// </summary>
public interface IProblemDetailsBuilderFactory
{
    /// <summary>
    /// Creates a new <see cref="IProblemDetailsBuilder"/> for the specified domain exception.
    /// </summary>
    /// <param name="e">The source domain exception.</param>
    /// <returns>The <see cref="IProblemDetailsBuilder"/> initialized with the exception.</returns>
    IProblemDetailsBuilder Create(DomainException e);
}