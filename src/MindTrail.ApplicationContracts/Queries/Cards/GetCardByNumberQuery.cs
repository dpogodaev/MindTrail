using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Interfaces.Queries;

namespace MindTrail.ApplicationContracts.Queries.Cards;

/// <summary>
/// Query for retrieving a single person by ID.
/// </summary>
/// <param name="Number">Gets the number of the card to retrieve.</param>
public sealed record GetCardByNumberQuery(int Number) : IQuery<CardDto?>;