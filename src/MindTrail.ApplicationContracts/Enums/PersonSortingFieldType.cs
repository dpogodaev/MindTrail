namespace MindTrail.ApplicationContracts.Enums;

/// <summary>
/// Specifies a field for sorting persons.
/// </summary>
public enum PersonSortingFieldType
{
    /// <summary>
    /// Sort by the person's full name.
    /// </summary>
    FullName,

    /// <summary>
    /// Sorting by the person's year of birth.
    /// </summary>
    BirthYear,

    /// <summary>
    /// Sorting by the time an entry was added.
    /// </summary>
    CreatedAt,
}