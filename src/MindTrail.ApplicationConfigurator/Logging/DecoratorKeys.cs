namespace MindTrail.ApplicationConfigurator.Logging;

/// <summary>
/// Keys used to register the "inner" (undecorated) implementation of a decorated service.
/// </summary>
internal static class DecoratorKeys
{
    /// <summary>
    /// Key for the innermost, undecorated implementation of a decorated service.
    /// </summary>
    public const string Inner = "inner";
}