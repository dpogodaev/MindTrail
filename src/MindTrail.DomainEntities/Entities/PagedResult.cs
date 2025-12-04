using System.Collections.Generic;

namespace MindTrail.DomainEntities.Entities;

/// <summary>
/// Represents a paged result containing a subset of items and information about pagination.
/// </summary>
/// <typeparam name="T">The type of the items in the result set.</typeparam>
public class PagedResult<T>
{
    /// <summary>
    /// Gets or sets the collection of items returned for the current page.
    /// </summary>
    public IEnumerable<T> Items { get; set; } = [];

    /// <summary>
    /// Gets or sets the total number of items.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Gets or sets the current page number (starting from 1).
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Gets or sets the number of items per page.
    /// </summary>
    public int PageSize { get; set; }
}