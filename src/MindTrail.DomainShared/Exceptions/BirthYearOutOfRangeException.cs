using MindTrail.DomainShared.Exceptions.Base;

namespace MindTrail.DomainShared.Exceptions;

/// <summary>
/// Thrown when the birth year is outside the valid range.
/// </summary>
public sealed class BirthYearOutOfRangeException(uint birthYear, uint minBirthYear)
    : DomainException(
        $"The birth year must be greater than {minBirthYear} " +
        $"and earlier than the current year. " +
        $"The specified value is {birthYear}).")
{
    /// <summary>
    /// Gets the specified value of the year of birth.
    /// </summary>
    public uint SpecifiedBirthYear { get; } = birthYear;

    /// <summary>
    /// Gets the minimum value of the year of birth.
    /// </summary>
    public uint MinBirthYear { get; } = minBirthYear;
}