using System.Collections.Generic;
using ThoughtGuide.ArchTests.Constants;
using ThoughtGuide.ArchTests.Extensions;
using ThoughtGuide.ArchTests.Helpers;
using Xunit;

namespace ThoughtGuide.ArchTests.ArchTests;

/// <summary>
/// Architectural tests for <see cref="ThoughtGuide.WebAuth"/> component.
/// </summary>
public class WebAuthArchTests
{
    private const string WorkingNamespace = ComponentNamespaces.WebAuth;

    /// <summary>
    /// Tests to check the dependency policy for <see cref="ThoughtGuide.WebAuth"/> component.
    /// </summary>
    [Fact]
    public void DependencyOfComponentsShouldFollowCleanArchitecture()
    {
        //Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(WorkingNamespace,
            "Components dependency policy",
            "Describes the dependencies of the 'WebAuth' component");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(WorkingNamespace)
                .ShouldNot().HaveDependenciesOtherThan(CreateAllowedDependenciesList([
                    ComponentNamespaces.Common
                ])),
            "The rule of dependence of the 'WebAuth' on other components",
            "The 'WebAuth' component can only depend on the 'Common' component " +
            "and should not have any dependencies on other components");

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        Assert.All(results, x => Assert.True(x.IsSuccessful));
    }

    /// <summary>
    /// Tests to check class naming of <see cref="ThoughtGuide.WebAuth"/> component.
    /// </summary>
    [Fact]
    public void ClassNamesMustFollowNamingRules()
    {
        //Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(WorkingNamespace,
            "File naming policy",
            "Describes the naming policy for files with the '.cs' extension");

        policyDefinition
            .AddAttributeNamingRule(WorkingNamespace)
            .AddConstantNamingRule(WorkingNamespace)
            .AddExtensionNamingRule(WorkingNamespace)
            .AddFilterNamingRule(WorkingNamespace)
            .AddHandlerNamingRule(WorkingNamespace)
            .AddInterfaceNamingRule(WorkingNamespace)
            .AddOptionNamingRule(WorkingNamespace)
            .AddSettingNamingRule(WorkingNamespace)
            .AddValidatorNamingRule(WorkingNamespace);

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        Assert.All(results, x => Assert.True(x.IsSuccessful));
    }

    #region Private methods

    private static string[] CreateAllowedDependenciesList(IEnumerable<string> allowedComponents)
    {
        var allowedDependenciesList = new List<string> { WorkingNamespace };
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