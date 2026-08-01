namespace MindTrail.ApplicationContracts.Enums;

/// <summary>
/// Specifies a field for sorting persons.
/// </summary>
public enum PersonSortingFieldType
{
    /// <summary>
    /// Sorting by full name.
    /// </summary>
    FullName,

    /// <summary>
    /// Sorting by year of birth.
    /// </summary>
    BirthYear,

    /// <summary>
    /// Sorting by the time an entry was added.
    /// </summary>
    CreatedAt,
}