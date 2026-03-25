using System;
using MindTrail.DomainShared.Exceptions;

namespace MindTrail.Domain.ValueObjects;

public class PersonFullName
{
    public const int MaxNameLength = 64;

    public PersonFullName(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        if (value.Length > MaxNameLength)
        {
            throw new PersonNameTooLongException(value, MaxNameLength);
        }

        Value = value.Trim();
    }

    public string Value { get; }

    public static implicit operator string(PersonFullName x) => x.Value;
}