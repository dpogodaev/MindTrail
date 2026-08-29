using MindTrail.DomainShared.Exceptions.Base;

namespace MindTrail.DomainShared.Exceptions;

/// <summary>
/// An exception thrown when the birth year is outside the valid range.
/// </summary>
/// <param name="birthYear">The specified year of birth.</param>
/// <param name="minBirthYear">The minimum valid year of birth.</param>
public sealed class BirthYearOutOfRangeException(int birthYear, int minBirthYear)
    : DomainException(
        $"The birth year must be greater than {minBirthYear} " +
        $"and earlier than the current year. " +
        $"The specified value is {birthYear}).")
{
    /// <summary>
    /// Gets the specified value of the year of birth.
    /// </summary>
    public int SpecifiedBirthYear { get; } = birthYear;

    /// <summary>
    /// Gets the minimum value of the year of birth.
    /// </summary>
    public int MinBirthYear { get; } = minBirthYear;
}