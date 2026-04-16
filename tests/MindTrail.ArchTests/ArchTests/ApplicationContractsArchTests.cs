using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.ArchTests.Constants;
using MindTrail.ArchTests.Extensions;
using MindTrail.ArchTests.Helpers;

namespace MindTrail.ArchTests.ArchTests;

/// <summary>
/// Architectural tests for <see cref="MindTrail.ApplicationContracts"/> component.
/// </summary>
[TestClass]
[TestCategory("Architecture")]
public class ApplicationContractsArchTests
{
    private const string CurrentNamespace = ComponentNamespaces.ApplicationContracts;

    private static readonly string[] UsingLibs =
    [
        "System"
    ];

    /// <summary>
    /// Verifies that the <see cref="MindTrail.ApplicationContracts"/> component follows the dependency rules.
    /// </summary>
    [TestMethod]
    public void Application_ShouldFollowDependencyRules()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            componentNamespace: CurrentNamespace,
            policyName: "Component dependency policy",
            policyDescription: $"Enforces the dependencies of the {nameof(ApplicationContracts)} component");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.Domain),
            name: "Restriction of dependency on Domain layer",
            description: "Application contracts should not have any dependencies on the domain core");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.Application),
            name: "Restriction of dependency on Application layer",
            description: "Application contracts should not have any dependencies on the application implementation");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.Cli,
                    ComponentNamespaces.WebApi,
                    ComponentNamespaces.WebAuth),
            name: "Restriction of dependency on Presentation layer",
            description: "Application contracts should not have any dependencies on the presentation layer");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.EfCore,
                    ComponentNamespaces.EfCoreMssql,
                    ComponentNamespaces.EfCorePostgreSql),
            name: "Restriction of dependency on Persistence layer",
            description: "Application contracts should not have any dependencies on the persistence layer");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.ApplicationConfigurator,
                    ComponentNamespaces.CliHost,
                    ComponentNamespaces.WebHost),
            name: "Restriction of dependency on Infrastructure layer",
            description: "Application contracts should not have any dependencies on the infrastructure layer");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependenciesOtherThan(
                    PolicyHelper.CreateAllowedDependenciesList(
                        CurrentNamespace,
                        UsingLibs,
                        [
                            ComponentNamespaces.DomainShared,
                            ComponentNamespaces.Common
                        ])),
            name: "Allowed dependencies",
            description: "Application contracts can only depend on shared domain types and common utilities");

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results)
        {
            Assert.IsTrue(result.IsSuccessful, PolicyHelper.BuildFailureMessage(result));
        }
    }

    /// <summary>
    /// Verifies that the <see cref="MindTrail.ApplicationContracts"/> component's types follow the naming conventions.
    /// </summary>
    [TestMethod]
    public void Application_ShouldFollowNamingConventions()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            componentNamespace: CurrentNamespace,
            policyName: "Type naming policy",
            policyDescription: $"Enforces naming conventions for types in the {nameof(ApplicationContracts)} component");

        policyDefinition
            .AddDtoNamingRule(CurrentNamespace)
            .AddInterfaceNamingRule(CurrentNamespace)
            .AddRequestModelNamingRule(CurrentNamespace);

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results)
        {
            Assert.IsTrue(result.IsSuccessful, PolicyHelper.BuildFailureMessage(result));
        }
    }
}