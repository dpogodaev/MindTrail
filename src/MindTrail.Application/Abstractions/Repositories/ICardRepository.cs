using System.Threading;
using System.Threading.Tasks;
using MindTrail.Domain.Entities;
using MindTrail.DomainShared.Exceptions.Cards;

namespace MindTrail.Application.Abstractions.Repositories;

/// <summary>
/// Provides data access operations for <see cref="Card"/> entities.
/// </summary>
public interface ICardRepository
{
    /// <summary>
    /// Returns a card by number.
    /// </summary>
    /// <param name="number">The number of the card to retrieve.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The card with the specified number, or <c>null</c> if not found.</returns>
    Task<Card?> GetCardByNumberAsync(
        int number,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the card with the specified number.
    /// </summary>
    /// <param name="number">The number of the card to retrieve.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The card with the specified number.</returns>
    /// <exception cref="CardNotFoundException">The card with the specified number was not found.</exception>
    Task<Card> GetRequiredCardByNumberAsync(
        int number,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new card.
    /// </summary>
    /// <param name="cardToCreate">The card to create.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The number of the created card.</returns>
    Task<int> CreateCardAsync(
        Card cardToCreate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing card.
    /// </summary>
    /// <param name="cardToUpdate">The card with the new values to persist.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The updated card.</returns>
    /// <exception cref="CardNotFoundException">The card to update was not found.</exception>
    Task<Card> UpdateCardAsync(
        Card cardToUpdate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a card by number.
    /// </summary>
    /// <param name="number">The number of the card to delete.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The deleted card.</returns>
    /// <exception cref="CardNotFoundException">The card with the specified number was not found.</exception>
    Task<Card> DeleteCardAsync(
        int number,
        CancellationToken cancellationToken = default);
}