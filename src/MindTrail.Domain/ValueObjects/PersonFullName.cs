using System;
using MindTrail.DomainShared.Exceptions;

namespace MindTrail.Domain.ValueObjects;

/// <summary>
/// The full name of a person.
/// </summary>
public sealed record PersonFullName
{
    /// <summary>
    /// The maximum allowed length of the full name.
    /// </summary>
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

    /// <summary>
    /// Gets the full name value.
    /// </summary>
    public string Value { get; }

    public static implicit operator string(PersonFullName x) => x.Value;

    public static PersonFullName Create(string value) => new(value);

    internal static PersonFullName FromPersistence(string value)
    {
        return new PersonFullName(value, true);
    }
}