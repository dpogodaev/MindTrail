using System.Collections.Generic;
using ThoughtGuide.ArchTests.Constants;
using ThoughtGuide.ArchTests.Extensions;
using ThoughtGuide.ArchTests.Helpers;
using Xunit;

namespace ThoughtGuide.ArchTests.ArchTests;

/// <summary>
/// Architectural tests for <see cref="ThoughtGuide.Common"/> component.
/// </summary>
public class CommonArchTests
{
    private const string WorkingNamespace = ComponentNamespaces.Common;

    /// <summary>
    /// Tests to check the dependency policy for <see cref="ThoughtGuide.Common"/> component.
    /// </summary>
    [Fact]
    public void DependencyOfComponentsShouldFollowCleanArchitecture()
    {
        //Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(WorkingNamespace,
            "Components dependency policy",
            "Describes the dependencies of the 'Common' component");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(WorkingNamespace)
                .ShouldNot().HaveDependenciesOtherThan(CreateAllowedDependenciesList()),
            "The rule of dependence of the 'Common' on other components",
            "The 'Common' component should not have any dependencies on other components");

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        Assert.All(results, x => Assert.True(x.IsSuccessful));
    }

    /// <summary>
    /// Tests to check class naming of <see cref="ThoughtGuide.Common"/> component.
    /// </summary>
    [Fact]
    public void ClassNamesMustFollowNamingRules()
    {
        //Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(WorkingNamespace,
            "File naming policy",
            "Describes the naming policy for files with the '.cs' extension");

        policyDefinition
            .AddExtensionNamingRule(WorkingNamespace)
            .AddInterfaceNamingRule(WorkingNamespace);

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        Assert.All(results, x => Assert.True(x.IsSuccessful));
    }

    #region Private methods

    private static string[] CreateAllowedDependenciesList()
    {
        var allowedDependenciesList = new List<string> { WorkingNamespace };
        allowedDependenciesList.AddRange(GetUsingLibs());

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