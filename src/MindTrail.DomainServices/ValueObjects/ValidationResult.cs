using System;

namespace MindTrail.DomainServices.ValueObjects;

/// <summary>
/// Validation result.
/// </summary>
public record ValidationResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationResult"/> class.
    /// </summary>
    public ValidationResult()
    {
        IsValid = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationResult"/> class.
    /// </summary>
    /// <param name="errorInfo">Error information.</param>
    public ValidationResult(string errorInfo)
    {
        IsValid = false;
        ErrorInfo = errorInfo;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationResult"/> class.
    /// </summary>
    /// <param name="errorInfo">Error information.</param>
    /// <param name="e">Exception details.</param>
    public ValidationResult(string errorInfo, Exception e)
    {
        IsValid = false;
        ErrorInfo = errorInfo;
        Exception = e;
    }

    /// <summary>
    /// Indicates if the validation was successful.
    /// </summary>
    public bool IsValid { get; }

    /// <summary>
    /// Error information.
    /// </summary>
    public string? ErrorInfo { get; }

    /// <summary>
    /// Exception details.
    /// </summary>
    public Exception? Exception { get; }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    public static ValidationResult WithSuccessful() => new();

    /// <summary>
    /// Creates an unsuccessful result with error information.
    /// </summary>
    /// <param name="errorInfo">Error information.</param>
    public static ValidationResult WithUnsuccessful(string errorInfo) => new(errorInfo);

    /// <summary>
    /// Creates an unsuccessful result with error information and exception details.
    /// </summary>
    /// <param name="errorInfo">Error information.</param>
    /// <param name="e">Exception details.</param>
    public static ValidationResult WithUnsuccessful(string errorInfo, Exception e) => new(errorInfo, e);
}