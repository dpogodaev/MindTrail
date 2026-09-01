using System;
using MindTrail.Domain.ValueObjects;

namespace MindTrail.Domain.Entities;

/// <summary>
/// Note-taking card.
/// </summary>
public class Card
{
    /// <summary>
    /// The identifier value used for a card that has not yet been persisted.
    /// </summary>
    private const int UnassignedId = 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="Card"/> class.
    /// </summary>
    /// <param name="id">A unique identifier. Optional.</param>
    /// <param name="title">The title.</param>
    /// <param name="content">The content. Optional.</param>
    /// <param name="createdAt">The date and time the card was created.</param>
    /// <param name="editedAt">The date and time the card was last edited. Optional.</param>
    private Card(
        int? id,
        CardTitle title,
        CardContent? content,
        DateTime createdAt,
        DateTime? editedAt = null)
    {
        Id = id ?? UnassignedId;
        Title = title;
        Content = content;
        CreatedAt = createdAt;
        EditedAt = editedAt;
    }

    /// <summary>
    /// Gets a unique identifier.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Gets the number.
    /// </summary>
    public int Number => Id;

    /// <summary>
    /// Gets the title.
    /// </summary>
    public CardTitle Title { get; private set; }

    /// <summary>
    /// Gets the content.
    /// </summary>
    public CardContent? Content { get; private set; }

    /// <summary>
    /// Gets the date and time the card was created.
    /// </summary>
    public DateTime CreatedAt { get; }

    /// <summary>
    /// Gets the date and time the card was last edited.
    /// </summary>
    public DateTime? EditedAt { get; private set; }

    /// <summary>
    /// Creates a new instance of the <see cref="Card"/> class.
    /// </summary>
    /// <param name="currentTime">The current time.</param>
    /// <param name="title">The title.</param>
    /// <param name="content">The content. Optional.</param>
    /// <returns>A new <see cref="Card"/> instance.</returns>
    public static Card Create(
        DateTime currentTime,
        CardTitle title,
        CardContent? content)
    {
        return new Card(null, title, content, currentTime);
    }

    /// <summary>
    /// Changes the title.
    /// </summary>
    /// <param name="title">The new title.</param>
    /// <param name="currentTime">The current time.</param>
    public void ChangeTitle(CardTitle title, DateTime currentTime)
    {
        Title = title;
        EditedAt = currentTime;
    }

    /// <summary>
    /// Changes the content.
    /// </summary>
    /// <param name="content">The new content.</param>
    /// <param name="currentTime">The current time.</param>
    public void ChangeContent(CardContent? content, DateTime currentTime)
    {
        Content = content;
        EditedAt = currentTime;
    }

    /// <summary>
    /// Restores a <see cref="Card"/> instance from persisted data.
    /// </summary>
    /// <param name="id">A unique identifier.</param>
    /// <param name="title">The title.</param>
    /// <param name="content">The content. Optional.</param>
    /// <param name="createdAt">The date and time the card was created.</param>
    /// <param name="editedAt">The date and time the card was last edited.</param>
    /// <returns>A <see cref="Card"/> instance restored from persistence.</returns>
    internal static Card FromPersistence(
        int id,
        string title,
        string? content,
        DateTime createdAt,
        DateTime? editedAt)
    {
        return new Card(
            id,
            CardTitle.FromPersistence(title),
            CardContent.FromPersistence(content),
            createdAt,
            editedAt);
    }
}