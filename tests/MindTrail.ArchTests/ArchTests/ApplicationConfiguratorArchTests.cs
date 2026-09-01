using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.ArchTests.Constants;
using MindTrail.ArchTests.Extensions;
using MindTrail.ArchTests.Helpers;

namespace MindTrail.ArchTests.ArchTests;

/// <summary>
/// Architectural tests for <see cref="MindTrail.ApplicationConfigurator"/> component.
/// </summary>
[TestClass]
[TestCategory("Architecture")]
public class ApplicationConfiguratorArchTests
{
    private const string CurrentNamespace = ComponentNamespaces.ApplicationConfigurator;

    private static readonly string[] UsingLibs =
    [
        "System",
        "Microsoft",
        "NLog"
    ];

    /// <summary>
    /// Ensures that the <see cref="MindTrail.ApplicationConfigurator"/> component follows the dependency rules.
    /// </summary>
    [TestMethod]
    public void ApplicationConfigurator_ShouldFollowDependencyRules()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            componentNamespace: CurrentNamespace,
            policyName: "Component dependency policy",
            policyDescription: $"Enforces the dependencies of the {nameof(ApplicationConfigurator)} component");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.Cli,
                    ComponentNamespaces.WebApi,
                    ComponentNamespaces.WebAuth),
            "Restriction of dependency on Presentation layer",
            "The application configurator should not have any dependencies on the presentation layer");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.CliHost,
                    ComponentNamespaces.WebHost),
            "Restriction of dependency on Infrastructure layer",
            $"The application configurator should not depend on any host implementations, such as ${nameof(WebHost)} or ${nameof(CliHost)}");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependenciesOtherThan(
                    PolicyHelper.CreateAllowedDependenciesList(
                        CurrentNamespace,
                        UsingLibs,
                        [
                            ComponentNamespaces.Domain,
                            ComponentNamespaces.DomainShared,
                            ComponentNamespaces.Application,
                            ComponentNamespaces.ApplicationContracts,
                            ComponentNamespaces.EfCore,
                            ComponentNamespaces.EfCoreMssql,
                            ComponentNamespaces.EfCorePostgreSql,
                            ComponentNamespaces.Common
                        ])),
            "Allowed dependencies",
            "The application configurator can only depend on the domain layer, application layer, persistence layer, and common utilities");

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results)
        {
            Assert.IsTrue(result.IsSuccessful, PolicyHelper.BuildFailureMessage(result));
        }
    }

    /// <summary>
    /// Ensures that the <see cref="MindTrail.ApplicationConfigurator"/> component's types follow the naming conventions.
    /// </summary>
    [TestMethod]
    public void ApplicationConfigurator_ShouldFollowNamingConventions()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            componentNamespace: CurrentNamespace,
            policyName: "Type naming policy",
            policyDescription: $"Enforces naming conventions for types in the {nameof(ApplicationConfigurator)} component");

        policyDefinition
            .AddAdapterNamingRule($"{CurrentNamespace}.Abstractions")
            .AddConfigNamingRule(CurrentNamespace)
            .AddExtensionNamingRule(CurrentNamespace)
            .AddHelperNamingRule(CurrentNamespace)
            .AddInterfaceNamingRule(CurrentNamespace)
            .AddProviderNamingRule(CurrentNamespace)
            .AddSettingNamingRule(CurrentNamespace);

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results)
        {
            Assert.IsTrue(result.IsSuccessful, PolicyHelper.BuildFailureMessage(result));
        }
    }
}