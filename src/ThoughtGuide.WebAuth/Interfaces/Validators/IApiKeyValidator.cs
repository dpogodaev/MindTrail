namespace ThoughtGuide.WebAuth.Interfaces.Validators;

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
    
    /// <summary>
    /// Indicates if the API key is not valid.
    /// </summary>
    /// <param name="apiKey">API key value.</param>
    /// <returns><c>true</c> if the API key is not valid; <c>false</c> otherwise.</returns>
    bool IsNotValid(string apiKey);
}