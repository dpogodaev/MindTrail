using System;
using System.IO;
using System.Reflection;

namespace ThoughtGuide.Common.Extensions;

/// <summary>
/// Extensions for working with an assembly and its attributes.
/// </summary>
public static class AssemblyExtensions
{
    private const string DefaultVersion = "0.0.0.0";
    private const string DefaultDateFormat = "yyyy-MM-ddTHH:mm:ss.fffffffZ"; // ISO 8601 format

    /// <summary>
    /// Returns the assembly version in the format <c>Major.Minor.Build.Revision</c>.
    /// </summary>
    /// <param name="assembly">An assembly.</param>
    /// <returns>The assembly version in the format <c>Major.Minor.Build.Revision</c>.</returns>
    /// <remarks>If the version is unavailable, <c>0.0.0.0</c> is returned by default.</remarks>
    public static string GetVersion(this Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        var version = assembly.GetName().Version;

        return version is not null
            ? $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}"
            : DefaultVersion;
    }

    /// <summary>
    /// Returns the assembly date in specified format.
    /// </summary>
    /// <param name="assembly">An assembly.</param>
    /// <param name="format">Date format. The default format is ISO 8601, <c>yyyy-MM-ddTHH:mm:ss.fffffffZ</c>.</param>
    /// <returns>The assembly date in the specified format; if the assembly file cannot be accessed, <c>null</c> is returned.</returns>
    /// <remarks>The time of the last write of the assembly file is used, which is usually the same as the date the assembly was last compiled or modified.</remarks>
    public static string GetAssemblyDate(this Assembly assembly, string format = DefaultDateFormat)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        return File.Exists(assembly.Location)
            ? File.GetLastWriteTimeUtc(assembly.Location).ToString(format)
            : null;
    }

    /// <summary>
    /// Returns the value of the configuration attribute of the assembly (e.g., "Debug" or "Release").
    /// </summary>
    /// <param name="assembly">An assembly.</param>
    /// <returns>The value of the configuration attribute of the assembly, or <c>null</c> if the attribute is not present.</returns>
    public static string GetAssemblyConfiguration(this Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        return assembly.GetCustomAttribute<AssemblyConfigurationAttribute>()?.Configuration;
    }

    /// <summary>
    /// Returns the value of the product attribute of the assembly (project name).
    /// </summary>
    /// <param name="assembly">An assembly.</param>
    /// <returns>The value of the product attribute of the assembly, or <c>null</c> if the attribute is not present.</returns>.
    public static string GetAssemblyProductName(this Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        return assembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product;
    }
}