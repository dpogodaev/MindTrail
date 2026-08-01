using System.ComponentModel.DataAnnotations;

namespace MindTrail.WebApi.Models.Cards;

/// <summary>
/// Model for creating a card with a note.
/// </summary>
public class CardCreationModel
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