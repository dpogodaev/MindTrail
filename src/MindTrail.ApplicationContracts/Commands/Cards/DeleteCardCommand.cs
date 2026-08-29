using MindTrail.ApplicationContracts.Interfaces.Commands;
using MindTrail.DomainShared.Exceptions.Cards;

namespace MindTrail.ApplicationContracts.Commands.Cards;

/// <summary>
/// Command for deleting a note-taking card.
/// </summary>
/// <exception cref="CardNotFoundException">The card with the specified number was not found.</exception>
public sealed record DeleteCardCommand : ICommand<VoidResult>
{
    /// <summary>
    /// Gets the number of the card to delete.
    /// </summary>
    public required int Number { get; init; }
}