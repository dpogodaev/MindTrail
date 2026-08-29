using System;
using MindTrail.DomainShared.Exceptions;

namespace MindTrail.Domain.ValueObjects;

/// <summary>
/// Birth year.
/// </summary>
public sealed record BirthYear
{
    /// <summary>
    /// The minimum valid birth year.
    /// </summary>
    public const int MinBirthYear = 1600;

    private BirthYear(int birthYear, DateTime currentTime)
    {
        if (birthYear < MinBirthYear || birthYear > currentTime.Year)
        {
            throw new BirthYearOutOfRangeException(birthYear, MinBirthYear);
        }

        Value = birthYear;
    }

    private BirthYear(int value) => Value = value;

    /// <summary>
    /// Gets the birth year value.
    /// </summary>
    public int Value { get; }

    public static implicit operator int(BirthYear x) => x.Value;

    public static implicit operator int?(BirthYear? x) => x?.Value;

    public static BirthYear Create(int birthYear, DateTime currentTime)
    {
        return new BirthYear(birthYear, currentTime);
    }

    public static BirthYear? Create(int? birthYear, DateTime currentTime)
    {
        return birthYear != null
            ? new BirthYear(birthYear.Value, currentTime)
            : null;
    }

    internal static BirthYear? FromPersistence(int? value)
    {
        return value == null ? null : new BirthYear(value.Value);
    }
}