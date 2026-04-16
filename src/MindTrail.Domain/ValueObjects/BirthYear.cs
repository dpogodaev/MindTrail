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

    public int Value { get; }

    public static implicit operator int(BirthYear x) => x.Value;
}