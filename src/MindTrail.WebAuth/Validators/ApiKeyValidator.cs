using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using MindTrail.WebAuth.Interfaces.Validators;
using MindTrail.WebAuth.Settings;

namespace MindTrail.WebAuth.Validators;

/// <inheritdoc cref="IApiKeyValidator"/>
public class ApiKeyValidator : IApiKeyValidator
{
    private readonly ApiKeySettings _settings;

    /// <summary>
    /// Initializes a new instance of <see cref="ApiKeyValidator"/> class.
    /// </summary>
    /// <param name="settings">API key settings.</param>
    public ApiKeyValidator(ApiKeySettings settings)
    {
        _settings = settings;
    }

    #region IApiKeyValidator

    /// <inheritdoc cref="IApiKeyValidator.IsValid"/>
    public bool IsValid(string apiKey)
    {
        if (IsApiKeyValid(apiKey, _settings.ApiKey)) return true;

        if (_settings.AdditionalApiKeys is null) return false;

        return _settings.AdditionalApiKeys.Any(additionalApiKey =>
            IsApiKeyValid(apiKey, additionalApiKey.Value));
    }

    /// <inheritdoc cref="IApiKeyValidator.IsNotValid"/>
    public bool IsNotValid(string apiKey)
    {
        return !IsValid(apiKey);
    }

    #endregion

    #region Private methods

    private static bool IsApiKeyValid(string receivedKey, string expectedKey)
    {
        return CompareKeysForFixedTimeToAvoidTimingAttacks(receivedKey, expectedKey);
    }

    private static bool CompareKeysForFixedTimeToAvoidTimingAttacks(string receivedKey, string expectedKey)
    {
        return CryptographicOperations.FixedTimeEquals(
            MemoryMarshal.Cast<char, byte>(receivedKey.AsSpan()),
            MemoryMarshal.Cast<char, byte>(expectedKey.AsSpan()));
    }

    #endregion
}