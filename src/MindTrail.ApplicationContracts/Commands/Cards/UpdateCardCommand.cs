using MindTrail.ApplicationContracts.Interfaces.Commands;
using MindTrail.DomainShared.Exceptions.Cards;

namespace MindTrail.ApplicationContracts.Commands.Cards;

/// <summary>
/// Command for updating a note-taking card.
/// </summary>
/// <exception cref="CardTitleTooLongException">The card's title is too long.</exception>
/// <exception cref="CardContentTooLongException">The card's content is too long.</exception>
public sealed record UpdateCardCommand : ICommand<VoidResult>
{
    /// <summary>
    /// Gets the number of the card to update.
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
}