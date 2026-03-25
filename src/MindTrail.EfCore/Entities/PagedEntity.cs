using System.Linq;
using MindTrail.EfCore.Interfaces.Entities;

namespace MindTrail.EfCore.Entities;

/// <summary>
/// Paged query result.
/// </summary>
/// <typeparam name="T">The type of items in the result set.</typeparam>
public sealed record PagedEntity<T>
    where T : IPersistentEntity
{
    /// <summary>
    /// Gets the total number of items that match the query.
    /// </summary>
    public long Total { get; init; }

    /// <summary>
    /// Gets the collection of items returned for the current page.
    /// </summary>
    public required IQueryable<T> Items { get; init; }
}