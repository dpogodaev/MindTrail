using System;
using MindTrail.DomainShared.Exceptions.Cards;

namespace MindTrail.Domain.ValueObjects;

/// <summary>
/// The content of a card.
/// </summary>
public sealed record CardContent
{
    public const int MaxLength = 8000;

    private CardContent(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        if (value.Length > MaxLength)
        {
            throw new CardContentTooLongException(value, MaxLength);
        }

        Value = value;
    }

    private CardContent(string value, bool isPersistence)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the content value.
    /// </summary>
    public string Value { get; }

    public static implicit operator string(CardContent x) => x.Value;

    public static CardContent? Create(string? value)
    {
        return value != null
            ? new CardContent(value)
            : null;
    }

    internal static CardContent? FromPersistence(string? value)
    {
        return value == null ? null : new CardContent(value, true);
    }
}