using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.ArchTests.Constants;
using MindTrail.ArchTests.Extensions;
using MindTrail.ArchTests.Helpers;

namespace MindTrail.ArchTests.ArchTests;

/// <summary>
/// Architectural tests for <see cref="MindTrail.WebAuth"/> component.
/// </summary>
[TestClass]
[TestCategory("Architecture")]
public class WebAuthArchTests
{
    private const string CurrentNamespace = ComponentNamespaces.WebAuth;

    /// <summary>
    /// Tests to check the dependency policy for <see cref="MindTrail.WebAuth"/> component.
    /// </summary>
    [TestMethod]
    public void DependencyOfComponentsShouldFollowCleanArchitecture()
    {
        //Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(CurrentNamespace,
            "Components dependency policy",
            $"Describes the dependencies of the ${nameof(WebAuth)} component");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependenciesOtherThan(
                    CreateAllowedDependenciesList([
                        ComponentNamespaces.Common
                    ])),
            $"The dependency rule of ${nameof(WebAuth)} on other components",
            $"The ${nameof(WebAuth)} component can only depend on the ${nameof(Common)} component " +
            "and should not have any dependencies on other components");

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results) Assert.IsTrue(result.IsSuccessful);
    }

    /// <summary>
    /// Tests to check class naming of <see cref="MindTrail.WebAuth"/> component.
    /// </summary>
    [TestMethod]
    public void ClassNamesMustFollowNamingRules()
    {
        //Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(CurrentNamespace,
            "Class naming policy",
            "Describes the naming policy for files with the '.cs' extension");

        policyDefinition
            .AddAttributeNamingRule(CurrentNamespace)
            .AddConstantNamingRule(CurrentNamespace)
            .AddExtensionNamingRule(CurrentNamespace)
            .AddFilterNamingRule(CurrentNamespace)
            .AddHandlerNamingRule(CurrentNamespace)
            .AddInterfaceNamingRule(CurrentNamespace)
            .AddOptionNamingRule(CurrentNamespace)
            .AddSettingNamingRule(CurrentNamespace)
            .AddValidatorNamingRule(CurrentNamespace);

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results) Assert.IsTrue(result.IsSuccessful);
    }

    #region Private methods

    private static string[] CreateAllowedDependenciesList(IEnumerable<string> allowedComponents)
    {
        var allowedDependenciesList = new List<string> { CurrentNamespace };
        allowedDependenciesList.AddRange(GetUsingLibs());
        allowedDependenciesList.AddRange(allowedComponents);

        return allowedDependenciesList.ToArray();
    }

    private static IEnumerable<string> GetUsingLibs()
    {
        return
        [
            "System",
            "Microsoft"
        ];
    }

    #endregion
}