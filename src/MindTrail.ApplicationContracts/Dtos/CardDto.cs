using System;

namespace MindTrail.ApplicationContracts.Dtos;

/// <summary>
/// Note-taking card.
/// </summary>
public class CardDto
{
    /// <summary>
    /// The number of the card.
    /// </summary>
    /// <remarks>
    /// Serves as the card's unique identifier.
    /// </remarks>
    public int Number { get; init; }

    /// <summary>
    /// The title of the card.
    /// </summary>
    public required string Title { get; init; }

    /// <summary>
    /// The content of the card.
    /// </summary>
    public string? Content { get; init; }

    /// <summary>
    /// The date and time the card was created.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// The date and time the card was last edited.
    /// </summary>
    public DateTime? EditedAt { get; init; }
}