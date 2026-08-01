using System;
using MindTrail.DomainShared.Exceptions.Cards;

namespace MindTrail.Domain.ValueObjects;

/// <summary>
/// The title of a card.
/// </summary>
public sealed record CardTitle
{
    public const int MaxLength = 200;

    private CardTitle(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        if (value.Length > MaxLength)
        {
            throw new CardTitleTooLongException(value, MaxLength);
        }

        Value = value.Trim();
    }

    private CardTitle(string value, bool isPersistence)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the title value.
    /// </summary>
    public string Value { get; }

    public static implicit operator string(CardTitle x) => x.Value;

    public static CardTitle Create(string value) => new(value);

    internal static CardTitle FromPersistence(string value)
    {
        return new CardTitle(value, true);
    }
}