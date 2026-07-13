namespace MindTrail.ApplicationContracts.Requests.Queries.Base;

public abstract record PagedQueryModel
{
    /// <summary>
    /// Gets a model for pagination.
    /// </summary>
    public required PaginationModel Pagination { get; init; } = new();

    /// <summary>
    /// Gets a model for text search across multiple fields.
    /// </summary>
    /// <remarks>
    /// If <c>null</c>, no text search is applied.
    /// </remarks>
    public TextSearchModel? Search { get; init; }
}