using System.Threading;
using System.Threading.Tasks;
using MindTrail.Domain.Entities;

namespace MindTrail.Application.Abstractions.Repositories;

public interface ICardRepository
{
    Task<Card?> GetCardByNumberAsync(
        int number,
        CancellationToken cancellationToken = default);

    Task<Card> GetRequiredCardByNumberAsync(
        int number,
        CancellationToken cancellationToken = default);

    Task<int> CreateCardAsync(
        Card cardToCreate,
        CancellationToken cancellationToken = default);

    Task<Card> UpdateCardAsync(
        Card cardToUpdate,
        CancellationToken cancellationToken = default);

    Task<Card> DeleteCardAsync(
        int number,
        CancellationToken cancellationToken = default);
}