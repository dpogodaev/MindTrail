namespace MindTrail.ApplicationContracts.Models;

/// <summary>
/// Pagination model.
/// </summary>
public sealed record PaginationModel
{
    private const int DefaultPageNumber = 1;
    private const int DefaultPageSize = 10;

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginationModel"/> class.
    /// </summary>
    /// <param name="pageNumber">The page number. If <c>null</c> or not greater than zero, defaults to <c>1</c>.</param>
    /// <param name="pageSize">The page size. If <c>null</c> or not greater than zero, defaults to <c>10</c>.</param>
    public PaginationModel(int? pageNumber = DefaultPageNumber, int? pageSize = DefaultPageSize)
    {
        PageNumber = pageNumber is null or <= 0
            ? DefaultPageNumber
            : pageNumber.Value;

        PageSize = pageSize is null or <= 0
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