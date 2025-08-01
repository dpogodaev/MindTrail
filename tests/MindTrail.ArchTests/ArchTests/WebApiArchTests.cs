using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.ArchTests.Constants;
using MindTrail.ArchTests.Extensions;
using MindTrail.ArchTests.Helpers;

namespace MindTrail.ArchTests.ArchTests;

/// <summary>
/// Architectural tests for <see cref="MindTrail.WebApi"/> component.
/// </summary>
[TestClass]
[TestCategory("Architecture")]
public class WebApiArchTests
{
    private const string CurrentNamespace = ComponentNamespaces.WebApi;

    private static readonly string[] UsingLibs =
    [
        "System",
        "Microsoft",
        "Swashbuckle"
    ];

    /// <summary>
    /// Tests to check the dependency policy for <see cref="MindTrail.WebApi"/> component.
    /// </summary>
    [TestMethod]
    public void DependencyOfComponentsShouldFollowCleanArchitecture()
    {
        //Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(CurrentNamespace,
            "Components dependency policy",
            $"Describes the dependencies of the ${nameof(WebApi)} component");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependenciesOtherThan(
                    CreateAllowedDependenciesList([
                        ComponentNamespaces.Common,
                        ComponentNamespaces.WebAuth
                    ])),
            $"The dependency rule of ${nameof(WebApi)} on other components",
            $"The ${nameof(WebApi)} component can only depend on the ${nameof(Common)} component " +
            $"and components that also implement the interface (e.g., ${nameof(WebAuth)})");

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results) Assert.IsTrue(result.IsSuccessful);
    }

    /// <summary>
    /// Tests to check class naming of <see cref="MindTrail.WebApi"/> component.
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
            .AddControllerNamingRule(CurrentNamespace)
            .AddFilterNamingRule(CurrentNamespace)
            .AddModelNamingRule(CurrentNamespace);

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