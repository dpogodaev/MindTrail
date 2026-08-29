using MindTrail.DomainShared.Exceptions.Base;

namespace MindTrail.DomainShared.Exceptions.Cards;

/// <summary>
/// An exception thrown when the card's content is too long.
/// </summary>
/// <param name="content">The specified content.</param>
/// <param name="maxLength">The maximum allowed length of the card's content.</param>
public sealed class CardContentTooLongException(string content, int maxLength)
    : DomainException(
        $"The maximum length of the card's content is {maxLength} characters. " +
        $"The length of the specified content is {content.Length}.")
{
    /// <summary>
    /// Gets the length of the specified content.
    /// </summary>
    public int SpecifiedContentLength { get; } = content.Length;

    /// <summary>
    /// Gets the maximum length of the card's content.
    /// </summary>
    public int MaxLength { get; } = maxLength;
}