using System;

namespace MindTrail.ApplicationContracts.Dtos;

/// <summary>
/// A card with a note.
/// </summary>
public class CardDto
{
    /// <summary>
    /// Gets the number.
    /// </summary>
    public int Number { get; init; }

    /// <summary>
    /// Gets the title.
    /// </summary>
    public required string Title { get; init; }

    /// <summary>
    /// Gets the content.
    /// </summary>
    public string? Content { get; init; }

    /// <summary>
    /// Gets the date and time the card was created.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Gets the date and time the card was last edited.
    /// </summary>
    public DateTime? EditedAt { get; init; }
}