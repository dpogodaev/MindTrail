using MindTrail.DomainShared.Exceptions.Base;

namespace MindTrail.DomainShared.Exceptions.Cards;

/// <summary>
/// An exception thrown when the card's title is too long.
/// </summary>
/// <param name="title">The specified title.</param>
/// <param name="maxLength">The maximum allowed length of the card's title.</param>
public sealed class CardTitleTooLongException(string title, int maxLength)
    : DomainException(
        $"The maximum length of the card's title is {maxLength} characters. " +
        $"The length of the specified title is {title.Length}.")
{
    /// <summary>
    /// Gets the length of the specified title.
    /// </summary>
    public int SpecifiedTitleLength { get; } = title.Length;

    /// <summary>
    /// Gets the maximum length of the card's title.
    /// </summary>
    public int MaxLength { get; } = maxLength;
}