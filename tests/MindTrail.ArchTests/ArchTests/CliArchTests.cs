using System.Collections.Generic;
using MindTrail.ArchTests.Constants;
using MindTrail.ArchTests.Extensions;
using MindTrail.ArchTests.Helpers;
using MindTrail.Cli.Commands;
using Xunit;

namespace MindTrail.ArchTests.ArchTests;

/// <summary>
/// Architectural tests for <see cref="MindTrail.Cli"/> component.
/// </summary>
public class CliArchTests
{
    private const string WorkingNamespace = ComponentNamespaces.Cli;

    /// <summary>
    /// Tests to check the dependency policy for <see cref="MindTrail.Cli"/> component.
    /// </summary>
    [Fact]
    public void DependencyOfComponentsShouldFollowCleanArchitecture()
    {
        //Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(WorkingNamespace,
            "Components dependency policy",
            "Describes the dependencies of the 'Cli' component");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(WorkingNamespace)
                .ShouldNot().HaveDependenciesOtherThan(CreateAllowedDependenciesList([
                    ComponentNamespaces.Common
                ])),
            "The rule of dependence of the 'Cli' on other components",
            "The 'Cli' component can only depend on the 'Common' component " +
            "and components that also implement the interface (there are no such components yet)");

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        Assert.All(results, x => Assert.True(x.IsSuccessful));
    }

    /// <summary>
    /// Tests to check class naming of <see cref="MindTrail.Cli"/> component.
    /// </summary>
    [Fact]
    public void ClassNamesMustFollowNamingRules()
    {
        //Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(WorkingNamespace,
            "File naming policy",
            "Describes the naming policy for files with the '.cs' extension");

        policyDefinition
            .AddCommandNamingRule(WorkingNamespace, [nameof(CommandControl)])
            .AddConstantNamingRule(WorkingNamespace)
            .AddFactoryNamingRule(WorkingNamespace)
            .AddHelperNamingRule(WorkingNamespace)
            .AddInterfaceNamingRule(WorkingNamespace)
            .AddServiceNamingRule(WorkingNamespace);

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