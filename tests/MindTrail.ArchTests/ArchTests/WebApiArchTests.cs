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
    /// Verifies that the <see cref="MindTrail.WebApi"/> component follows the dependency rules.
    /// </summary>
    [TestMethod]
    public void WebApi_ShouldFollowDependencyRules()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            componentNamespace: CurrentNamespace,
            policyName: "Component dependency policy",
            policyDescription: $"Enforces the dependencies of the {nameof(WebApi)} component");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.Domain),
            name: "Restriction of dependency on Domain layer",
            description: "The Web API should not have any dependencies on the domain core");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.Application),
            name: "Restriction of dependency on Application layer",
            description: "The Web API should not have any dependencies on the application implementation");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.Cli),
            name: "Restriction of dependency on Presentation layer",
            description: "The Web API should not depend on other presentation implementations, such as command-line-based presentation components");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.EfCore,
                    ComponentNamespaces.EfCoreMssql,
                    ComponentNamespaces.EfCorePostgreSql),
            name: "Restriction of dependency on Persistence layer",
            description: "The Web API should not have any dependencies on the persistence layer");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.HostConfiguration,
                    ComponentNamespaces.CliHost,
                    ComponentNamespaces.WebHost),
            name: "Restriction of dependency on Infrastructure layer",
            description: "The Web API should not have any dependencies on the infrastructure layer");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependenciesOtherThan(
                    PolicyHelper.CreateAllowedDependenciesList(
                        CurrentNamespace,
                        UsingLibs,
                        [
                            ComponentNamespaces.WebAuth,
                            ComponentNamespaces.ApplicationContracts,
                            ComponentNamespaces.DomainShared,
                            ComponentNamespaces.Common
                        ])),
            name: "Allowed dependencies",
            description: "The Web API can only depend on components that also implement the web interface, application contracts, shared domain types, and common utilities");

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results)
        {
            Assert.IsTrue(result.IsSuccessful, PolicyHelper.BuildFailureMessage(result));
        }
    }

    /// <summary>
    /// Verifies that the <see cref="MindTrail.WebApi"/> component's types follow the naming conventions.
    /// </summary>
    [TestMethod]
    public void WebApi_ShouldFollowNamingConventions()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            componentNamespace: CurrentNamespace,
            policyName: "Type naming policy",
            policyDescription: $"Enforces naming conventions for types in the {nameof(WebApi)} component");

        policyDefinition
            .AddAbstractionNamingRule(CurrentNamespace)
            .AddBuilderNamingRule($"{CurrentNamespace}.Abstractions")
            .AddFactoryNamingRule($"{CurrentNamespace}.Abstractions")
            .AddProviderNamingRule($"{CurrentNamespace}.Abstractions")
            .AddAttributeNamingRule(CurrentNamespace)
            .AddControllerNamingRule(CurrentNamespace)
            .AddDtoNamingRule(CurrentNamespace)
            .AddFilterNamingRule(CurrentNamespace)
            .AddHandlerNamingRule(CurrentNamespace)
            .AddInterfaceNamingRule(CurrentNamespace)
            .AddMappingNamingRule(CurrentNamespace)
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