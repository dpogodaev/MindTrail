using MindTrail.DomainServices.Exceptions.Base;

namespace MindTrail.WebApi.Interfaces.Providers;

/// <summary>
/// Provides an error code associated with a given domain exception.
/// </summary>
public interface IErrorCodeProvider
{
    /// <summary>
    /// Tries to provide an error code for the specified domain exception.
    /// </summary>
    /// <param name="e">The domain exception.</param>
    /// <param name="code">The resulting error code if found; <c>null</c> otherwise.</param>
    /// <returns><c>true</c> if the error code is successfully provided; <c>false</c> otherwise.</returns>
    bool TryGetCode(DomainException e, out string? code);
}