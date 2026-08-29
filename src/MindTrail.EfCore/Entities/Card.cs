using System;
using System.ComponentModel.DataAnnotations.Schema;
using MindTrail.EfCore.Interfaces.Entities;

namespace MindTrail.EfCore.Entities;

/// <summary>
/// Note-taking card.
/// </summary>
[Table("Cards")]
public class Card : IPersistentEntity, IHasCreationTime, IHasModificationTime
{
    /// <summary>
    /// Gets a unique identifier (primary key).
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Gets or sets the content.
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// Gets the date and time the card was created.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Gets or sets the date and time the card was last edited.
    /// </summary>
    public DateTime? EditedAt { get; set; }

    /// <inheritdoc/>
    public DateTime CreationTime { get; set; }

    /// <inheritdoc/>
    public DateTime? LastModificationTime { get; set; }
}