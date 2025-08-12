using System;

namespace MindTrail.AppServices.Exceptions;

/// <summary>
/// Thrown when the property value conflicts with the current state of the target resource.
/// </summary>
public sealed class InvalidStateException(string msg, string? propName = null, string? propValue = null)
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