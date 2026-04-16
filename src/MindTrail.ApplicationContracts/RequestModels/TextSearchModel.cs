namespace MindTrail.ApplicationContracts.RequestModels;

/// <summary>
/// Model for text search across multiple fields.
/// </summary>
public record TextSearchModel
{
    public const bool DefaultCaseSensitive = false;

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
    /// </summary>
    public bool CaseSensitive { get; }
}