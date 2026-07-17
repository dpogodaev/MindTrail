using System;
using MindTrail.DomainShared.Exceptions;

namespace MindTrail.Domain.ValueObjects;

/// <summary>
/// Birth year.
/// </summary>
public sealed record BirthYear
{
    public const int MinBirthYear = 1600;

    public BirthYear(int birthYear, DateTime currentTime)
    {
        if (birthYear < MinBirthYear || birthYear > currentTime.Year)
        {
            throw new BirthYearOutOfRangeException(birthYear, MinBirthYear);
        }

        Value = birthYear;
    }

    private BirthYear(int value) => Value = value;

    public int Value { get; }

    public static implicit operator int(BirthYear x) => x.Value;

    public static implicit operator int?(BirthYear? x) => x?.Value;

    public static BirthYear? Create(int? birthYear, DateTime currentTime)
    {
        return birthYear != null
            ? new BirthYear(birthYear.Value, currentTime)
            : null;
    }

    public static BirthYear? FromPersistence(int? value)
    {
        return value == null ? null : new BirthYear(value.Value);
    }
}