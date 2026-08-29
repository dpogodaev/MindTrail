using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using MindTrail.WebAuth.Interfaces.Validators;
using MindTrail.WebAuth.Settings;

namespace MindTrail.WebAuth.Validators;

/// <inheritdoc/>
/// <param name="settings">The API key settings.</param>
public class ApiKeyValidator(ApiKeySettings settings)
    : IApiKeyValidator
{
    /// <inheritdoc/>
    public bool IsValid(string apiKey)
    {
        if (IsApiKeyValid(apiKey, settings.ApiKey))
        {
            return true;
        }

        if (settings.AdditionalApiKeys is null)
        {
            return false;
        }

        return settings.AdditionalApiKeys.Any(additionalApiKey =>
            IsApiKeyValid(apiKey, additionalApiKey.Value));
    }

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
}