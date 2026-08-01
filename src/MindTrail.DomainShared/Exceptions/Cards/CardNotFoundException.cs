using MindTrail.DomainShared.Exceptions.Base;

namespace MindTrail.DomainShared.Exceptions.Cards;

/// <summary>
/// Exception thrown when the card was not found.
/// </summary>
/// <param name="number">The card number.</param>
public sealed class CardNotFoundException(int number)
    : DomainException($"The card with the number {number} was not found.")
{
    /// <summary>
    /// Gets the number.
    /// </summary>
    public int Number { get; } = number;
}