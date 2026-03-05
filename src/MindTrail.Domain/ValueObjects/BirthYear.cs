using System;
using MindTrail.DomainShared.Exceptions;

namespace MindTrail.Domain.ValueObjects;

/// <summary>
/// Birth year.
/// </summary>
public sealed record BirthYear
{
    private const int MinBirthYearInYears = 1600;

    public BirthYear(int birthYear, DateTime currentTime)
        : this(checked((uint)birthYear), currentTime)
    {
    }

    public BirthYear(uint birthYear, DateTime currentTime)
    {
        if (birthYear < MinBirthYearInYears || birthYear > currentTime.Year)
        {
            throw new BirthYearOutOfRangeException(birthYear, MinBirthYearInYears);
        }

        Value = birthYear;
    }

    public uint Value { get; }

    public static explicit operator int(BirthYear x) => checked((int)x.Value);

    public static implicit operator uint(BirthYear x) => x.Value;
}