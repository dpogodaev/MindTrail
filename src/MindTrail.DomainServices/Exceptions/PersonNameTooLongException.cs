using MindTrail.DomainServices.Exceptions.Base;

namespace MindTrail.DomainServices.Exceptions;

/// <summary>
/// An exception thrown when the person's name is too long.
/// </summary>
public sealed class PersonNameTooLongException(string fullName, int maxLength)
    : DomainException(
        $"The maximum length of the person's name is {maxLength} characters " +
        $"(the current value is {fullName.Length}).")
{
    /// <summary>
    /// Gets maximum API key length.
    /// </summary>
    public int MaxLength { get; } = maxLength;
}