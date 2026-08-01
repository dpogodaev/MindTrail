using System.ComponentModel.DataAnnotations;

namespace MindTrail.WebApi.Models.Cards;

/// <summary>
/// Model for updating a card with a note.
/// </summary>
public record CardUpdateModel
{
    /// <summary>
    /// Gets the title.
    /// </summary>
    [Required]
    public required string Title { get; init; }

    /// <summary>
    /// Gets the content.
    /// </summary>
    public string? Content { get; init; }
}