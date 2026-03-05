using System.Collections.Generic;

namespace MindTrail.ApplicationContracts.Dtos;

/// <summary>
/// Paged query result.
/// </summary>
/// <typeparam name="T">The type of items in the result set.</typeparam>
public sealed record PagedDto<T>
    where T : class
{
    /// <summary>
    /// Gets the total number of items that match the query.
    /// </summary>
    public long Total { get; init; }

    /// <summary>
    /// Gets the collection of items returned for the current page.
    /// </summary>
    public IReadOnlyCollection<T> Items { get; init; } = [];
}