namespace MindTrail.ApplicationContracts.RequestModels;

/// <summary>
/// Pagination model.
/// </summary>
public record PaginationModel
{
    public const int DefaultPageNumber = 1;
    public const int DefaultPageSize = 10;

    public PaginationModel(int? pageNumber = DefaultPageNumber, int? pageSize = DefaultPageSize)
    {
        PageNumber = pageNumber is null or 0
            ? DefaultPageNumber
            : pageNumber.Value;

        PageSize = pageSize is null or 0
            ? DefaultPageSize
            : pageSize.Value;
    }

    /// <summary>
    /// Gets the page number.
    /// The default value is <c>1</c>.
    /// </summary>
    public int PageNumber { get; }

    /// <summary>
    /// Gets the page size.
    /// The default value is <c>10</c>.
    /// </summary>
    public int PageSize { get; }
}