using System;

namespace MindTrail.DomainServices.Exceptions.Base;

/// <summary>
/// Handled exception.
/// </summary>
/// <param name="message">The error message that explains the reason for the exception.</param>
public abstract class DomainException(string message) : Exception(message);