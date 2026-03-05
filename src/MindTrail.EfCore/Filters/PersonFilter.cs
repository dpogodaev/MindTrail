using MindTrail.EfCore.Filters.Base;

namespace MindTrail.EfCore.Filters;

public record PersonFilter : BaseFilter
{
    /// <summary>
    /// Gets the full name.
    /// </summary>
    public string? FullName { get; init; }

    /// <summary>
    /// Gets the year of birth.
    /// </summary>
    public uint? BirthYear { get; init; }
}