using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.ArchTests.Constants;
using MindTrail.ArchTests.Extensions;
using MindTrail.ArchTests.Helpers;

namespace MindTrail.ArchTests.ArchTests;

/// <summary>
/// Architectural tests for <see cref="MindTrail.CliHost"/> component.
/// </summary>
[TestClass]
[TestCategory("Architecture")]
public class CliHostArchTests
{
    private const string CurrentNamespace = ComponentNamespaces.CliHost;

    private static readonly string[] UsingLibs =
    [
        "System",
        "Microsoft"
    ];

    /// <summary>
    /// Verifies that the <see cref="MindTrail.CliHost"/> component follows the dependency rules.
    /// </summary>
    [TestMethod]
    public void CliHost_ShouldFollowDependencyRules()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            componentNamespace: CurrentNamespace,
            policyName: "Component dependency policy",
            policyDescription: $"Enforces the dependencies of the {nameof(CliHost)} component");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.Domain),
            name: "Restriction of dependency on Domain layer",
            description: "The CLI host should not have any dependencies on the domain core");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.Application,
                    ComponentNamespaces.ApplicationContracts),
            name: "Restriction of dependency on Application layer",
            description: "The CLI host should not have any dependencies on the application layer");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.WebApi,
                    ComponentNamespaces.WebAuth),
            name: "Restriction of dependency on Presentation layer",
            description: $"The CLI host should not depend on web-based presentation components such as {nameof(WebApi)}");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.EfCore,
                    ComponentNamespaces.EfCoreMssql,
                    ComponentNamespaces.EfCorePostgreSql),
            name: "Restriction of dependency on Persistence layer",
            description: "The CLI host should not have any dependencies on persistence layer");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.WebHost),
            name: "Restriction of dependency on Infrastructure layer",
            description: $"The CLI should not have any dependencies on other infrastructure implementations, such as {nameof(WebHost)}");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependenciesOtherThan(
                    PolicyHelper.CreateAllowedDependenciesList(
                        CurrentNamespace,
                        UsingLibs,
                        [
                            ComponentNamespaces.Cli,
                            ComponentNamespaces.HostConfiguration,
                            ComponentNamespaces.DomainShared,
                            ComponentNamespaces.Common
                        ])),
            name: "Allowed dependencies",
            description: "The CLI host can only depend on the components implementing its interface, the application configurator, shared domain types, and common utilities");

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results)
        {
            Assert.IsTrue(result.IsSuccessful, PolicyHelper.BuildFailureMessage(result));
        }
    }

    /// <summary>
    /// Verifies that the <see cref="MindTrail.CliHost"/> component's types follow the naming conventions.
    /// </summary>
    [TestMethod]
    public void CliHost_ShouldFollowNamingConventions()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            componentNamespace: CurrentNamespace,
            policyName: "Type naming policy",
            policyDescription: $"Enforces naming conventions for types in the {nameof(CliHost)} component");

        policyDefinition.AddConfigNamingRule(CurrentNamespace);

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results)
        {
            Assert.IsTrue(result.IsSuccessful, PolicyHelper.BuildFailureMessage(result));
        }
    }
}