using System;

namespace MindTrail.AppServices.Exceptions;

/// <summary>
/// Thrown when the property has invalid value.
/// </summary>
public sealed class InvalidValueException(string msg, string? propName = null, string? propValue = null)
    : Exception(msg)
{
    /// <summary>
    /// Property name.
    /// </summary>
    public string? PropertyName { get; } = propName;

    /// <summary>
    /// Property value.
    /// </summary>
    public string? PropertyValue { get; } = propValue;
}