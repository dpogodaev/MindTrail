namespace MindTrail.WebApi.RequestModels;

/// <summary>
/// Request model containing filtering and pagination parameters to get a collection of countries.
/// </summary>
public record CountryFilterModel
{
    /// <summary>
    /// Gets or sets a filter for the country name. Optional.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the page number. Optional.
    /// The default value is <c>1</c>.
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Gets or sets the page size. Optional.
    /// The default value is <c>10</c>.
    /// </summary>
    public int PageSize { get; set; } = 10;
}