using System.Collections.Generic;
using MindTrail.ArchTests.Constants;
using MindTrail.ArchTests.Extensions;
using MindTrail.ArchTests.Helpers;
using Xunit;

namespace MindTrail.ArchTests.ArchTests;

/// <summary>
/// Architectural tests for <see cref="MindTrail.WebHost"/> component.
/// </summary>
public class WebHostArchTests
{
    private const string WorkingNamespace = ComponentNamespaces.WebHost;

    /// <summary>
    /// Tests to check the dependency policy for <see cref="MindTrail.WebHost"/> component.
    /// </summary>
    [Fact]
    public void DependencyOfComponentsShouldFollowCleanArchitecture()
    {
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(WorkingNamespace,
            "Components dependency policy",
            "Describes the dependencies of the 'WebHost' component");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(WorkingNamespace)
                .ShouldNot().HaveDependenciesOtherThan(CreateAllowedDependenciesList([
                    ComponentNamespaces.HostConfiguration,
                    ComponentNamespaces.WebApi,
                    ComponentNamespaces.WebAuth
                ])),
            "The rule of dependence of the 'WebHost' on other components",
            "The 'WebHost' component can only depend on the components implementing its interface (e.g., 'WebApi' and 'WebAuth') " +
            "and the application configurator ('HostConfiguration')");

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        Assert.All(results, x => Assert.True(x.IsSuccessful));
    }

    /// <summary>
    /// Tests to check class naming of <see cref="MindTrail.WebHost"/> component.
    /// </summary>
    [Fact]
    public void ClassNamesMustFollowTheNamingRules()
    {
        //Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(WorkingNamespace,
            "File naming policy",
            "Describes the naming policy for files with the '.cs' extension");

        policyDefinition
            .AddConfigNamingRule(WorkingNamespace)
            .AddSettingNamingRule(WorkingNamespace);

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
            "Microsoft",
            "Swashbuckle",
            "AutoMapper"
        ];
    }

    #endregion
}