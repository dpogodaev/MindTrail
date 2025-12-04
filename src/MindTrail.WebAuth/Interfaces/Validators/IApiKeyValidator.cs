namespace MindTrail.WebAuth.Interfaces.Validators;

/// <summary>
/// Validator for API key.
/// </summary>
public interface IApiKeyValidator
{
    /// <summary>
    /// Indicates if the API key is valid.
    /// </summary>
    /// <param name="apiKey">API key value.</param>
    /// <returns><c>true</c> if the API key is valid; <c>false</c> otherwise.</returns>
    bool IsValid(string apiKey);
}