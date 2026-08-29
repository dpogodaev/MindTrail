using System.ComponentModel.DataAnnotations;

namespace MindTrail.WebApi.Models.Cards;

/// <summary>
/// Model for updating a note-taking card.
/// </summary>
public sealed record CardUpdateModel
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