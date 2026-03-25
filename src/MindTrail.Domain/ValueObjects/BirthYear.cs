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
        : this(checked((uint)birthYear), currentTime)
    {
    }

    public BirthYear(uint birthYear, DateTime currentTime)
    {
        if (birthYear < MinBirthYear || birthYear > currentTime.Year)
        {
            throw new BirthYearOutOfRangeException(birthYear, MinBirthYear);
        }

        Value = birthYear;
    }

    public uint Value { get; }

    public static explicit operator int(BirthYear x) => checked((int)x.Value);

    public static implicit operator uint(BirthYear x) => x.Value;
}