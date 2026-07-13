namespace MindTrail.ApplicationContracts.Requests.Queries;

/// <summary>
/// Model for text search across multiple fields.
/// </summary>
public sealed record TextSearchModel
{
    private const bool DefaultCaseSensitive = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="TextSearchModel"/> class.
    /// </summary>
    /// <param name="query">The text search query.</param>
    /// <param name="caseSensitive">A value indicating whether the text search should be case-sensitive.</param>
    /// <remarks>If <paramref name="caseSensitive"/> is <c>null</c>, the search is case-insensitive.</remarks>
    public TextSearchModel(string query, bool? caseSensitive = DefaultCaseSensitive)
    {
        Query = query;
        CaseSensitive = caseSensitive ?? DefaultCaseSensitive;
    }

    /// <summary>
    /// Gets a text search query.
    /// </summary>
    public string Query { get; }

    /// <summary>
    /// Gets a value indicating whether the text search should be case-sensitive.
    /// The default value is <c>false</c>.
    /// </summary>
    public bool CaseSensitive { get; }
}