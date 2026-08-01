using System;
using MindTrail.DomainShared.Exceptions;

namespace MindTrail.Domain.ValueObjects;

public sealed record PersonFullName
{
    public const int MaxNameLength = 64;

    private PersonFullName(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        if (value.Length > MaxNameLength)
        {
            throw new PersonNameTooLongException(value, MaxNameLength);
        }

        Value = value.Trim();
    }

    private PersonFullName(string value, bool isPersistence)
    {
        Value = value;
    }

    public string Value { get; }

    public static implicit operator string(PersonFullName x) => x.Value;

    public static PersonFullName Create(string value) => new(value);

    internal static PersonFullName FromPersistence(string value)
    {
        return new PersonFullName(value, true);
    }
}