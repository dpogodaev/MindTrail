using System.ComponentModel.DataAnnotations;

namespace MindTrail.WebApi.Models.Cards;

/// <summary>
/// Model for creating a note-taking card.
/// </summary>
public sealed record CardCreationModel
{
    /// <summary>
    /// The title of the card.
    /// </summary>
    [Required]
    public required string Title { get; init; }

    /// <summary>
    /// The content of the card.
    /// </summary>
    public string? Content { get; init; }
}