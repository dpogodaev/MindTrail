using System.Collections.Generic;
using System.Reflection;

namespace ThoughtGuide.HostConfiguration.Helpers;

/// <summary>
/// Assembly helper.
/// </summary>
public class AssemblyHelper
{
    /// <summary>
    /// Returns a list of application assembly names.
    /// </summary>
    /// <param name="namePrefix">Prefix of the assembly name for filtering.</param>
    /// <returns>List of application assembly names</returns>
    public static IEnumerable<string> GetAssemblyNames(string namePrefix = null)
    {
        var assemblyNames = new HashSet<string>();
        var assembliesToScan = new Queue<Assembly>();

        var rootAssembly = Assembly.GetEntryAssembly();
        assembliesToScan.Enqueue(rootAssembly);

        while (assembliesToScan.Count != 0)
        {
            var assembly = assembliesToScan.Dequeue();

            if (AssemblyDoesNotMatchPrefix(assembly.FullName)) continue;

            assemblyNames.Add(assembly.FullName);

            var references = assembly.GetReferencedAssemblies();
            foreach (var reference in references)
            {
                if (assemblyNames.Contains(reference.FullName)) continue;
                if (AssemblyDoesNotMatchPrefix(reference.FullName)) continue;

                assembliesToScan.Enqueue(Assembly.Load(reference));
            }
        }

        bool AssemblyDoesNotMatchPrefix(string assemblyName)
        {
            return !string.IsNullOrEmpty(namePrefix) && assemblyName.StartsWith(namePrefix) == false;
        }

        return assemblyNames;
    }
}