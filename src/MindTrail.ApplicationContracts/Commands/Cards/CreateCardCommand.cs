using MindTrail.ApplicationContracts.Interfaces.Commands;
using MindTrail.DomainShared.Exceptions.Cards;

namespace MindTrail.ApplicationContracts.Commands.Cards;

/// <summary>
/// Command for creating a card with a note.
/// </summary>
/// <returns>The number of the created card.</returns>
/// <exception cref="CardTitleTooLongException">The card's title is too long.</exception>
/// <exception cref="CardContentTooLongException">The card's content is too long.</exception>
public class CreateCardCommand : ICommand<int>
{
    /// <summary>
    /// Gets the title.
    /// </summary>
    public required string Title { get; init; }

    /// <summary>
    /// Gets the content.
    /// </summary>
    public string? Content { get; init; }
}