using MindTrail.DomainShared.Exceptions.Base;

namespace MindTrail.DomainShared.Exceptions;

/// <summary>
/// An exception thrown when the person's name is too long.
/// </summary>
public sealed class PersonNameTooLongException(string fullName, int maxLength)
    : DomainException(
        $"The maximum length of the person's name is {maxLength} characters. " +
        $"The length of the specified name is {fullName.Length}.")
{
    /// <summary>
    /// Gets the length of the specified name.
    /// </summary>
    public int SpecifiedNameLength { get; } = fullName.Length;

    /// <summary>
    /// Gets the maximum length of the person's name.
    /// </summary>
    public int MaxLength { get; } = maxLength;
}