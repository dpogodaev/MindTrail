using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Interfaces.Queries;

namespace MindTrail.ApplicationContracts.Queries.Cards;

/// <summary>
/// Query for retrieving a single card by number.
/// </summary>
/// <param name="Number">The number of the card to retrieve.</param>
public sealed record GetCardByNumberQuery(int Number) : IQuery<CardDto?>;