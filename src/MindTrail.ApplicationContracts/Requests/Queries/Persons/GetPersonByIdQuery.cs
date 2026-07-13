using System;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Interfaces.Queries;

namespace MindTrail.ApplicationContracts.Requests.Queries.Persons;

/// <summary>
/// Query for retrieving a single person by its identifier.
/// </summary>
/// <param name="Id">Gets the identifier of the person to retrieve.</param>
public sealed record GetPersonByIdQuery(Guid Id) : IQuery<PersonDto?>;