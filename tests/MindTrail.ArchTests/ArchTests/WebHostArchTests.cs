using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.ArchTests.Constants;
using MindTrail.ArchTests.Extensions;
using MindTrail.ArchTests.Helpers;

namespace MindTrail.ArchTests.ArchTests;

/// <summary>
/// Architectural tests for <see cref="MindTrail.WebHost"/> component.
/// </summary>
[TestClass]
[TestCategory("Architecture")]
public class WebHostArchTests
{
    private const string CurrentNamespace = ComponentNamespaces.WebHost;

    private static readonly string[] UsingLibs =
    [
        "System",
        "Microsoft",
        "Swashbuckle"
    ];

    /// <summary>
    /// Tests to check the dependency policy for <see cref="MindTrail.WebHost"/> component.
    /// </summary>
    [TestMethod]
    public void DependencyOfComponentsShouldFollowCleanArchitecture()
    {
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(CurrentNamespace,
            "Components dependency policy",
            $"Describes the dependencies of the ${nameof(WebHost)} component");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependenciesOtherThan(
                    CreateAllowedDependenciesList([
                        ComponentNamespaces.HostConfiguration,
                        ComponentNamespaces.WebApi,
                        ComponentNamespaces.WebAuth
                    ])),
            $"The dependency rule of ${nameof(WebHost)} on other components",
            $"The ${nameof(WebHost)} component can only depend on the components implementing its interface " +
            $"(e.g., ${nameof(WebApi)} and ${nameof(WebAuth)}) " +
            $"and the application configurator (${nameof(HostConfiguration)})");

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results) Assert.IsTrue(result.IsSuccessful);
    }

    /// <summary>
    /// Tests to check class naming of <see cref="MindTrail.WebHost"/> component.
    /// </summary>
    [TestMethod]
    public void ClassNamesMustFollowTheNamingRules()
    {
        //Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(CurrentNamespace,
            "Class naming policy",
            "Describes the naming policy for files with the '.cs' extension");

        policyDefinition
            .AddConfigNamingRule(CurrentNamespace)
            .AddSettingNamingRule(CurrentNamespace);

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results) Assert.IsTrue(result.IsSuccessful);
    }

    #region Private methods

    private static string[] CreateAllowedDependenciesList(IEnumerable<string> allowedComponents)
    {
        var allowedDependenciesList = new List<string> { CurrentNamespace };
        allowedDependenciesList.AddRange(UsingLibs);
        allowedDependenciesList.AddRange(allowedComponents);

        return allowedDependenciesList.ToArray();
    }

    #endregion
}