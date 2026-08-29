using MindTrail.DomainShared.Exceptions.Base;

namespace MindTrail.DomainShared.Exceptions;

/// <summary>
/// An exception thrown when the country was not found.
/// </summary>
/// <param name="id">The country's ID.</param>
public sealed class CountryNotFoundException(int id)
    : DomainException($"The country with identifier '{id}' was not found.")
{
    /// <summary>
    /// Gets the country's ID.
    /// </summary>
    public int Id { get; } = id;
}