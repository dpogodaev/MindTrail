using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.ArchTests.Constants;
using MindTrail.ArchTests.Extensions;
using MindTrail.ArchTests.Helpers;

namespace MindTrail.ArchTests.ArchTests;

/// <summary>
/// Architectural tests for <see cref="MindTrail.HostConfiguration"/> component.
/// </summary>
[TestClass]
[TestCategory("Architecture")]
public class HostConfigurationArchTests
{
    private const string CurrentNamespace = ComponentNamespaces.HostConfiguration;

    private static readonly string[] UsingLibs =
    [
        "System",
        "Microsoft",
        "NLog"
    ];

    /// <summary>
    /// Verifies that the <see cref="MindTrail.HostConfiguration"/> component follows the dependency rules.
    /// </summary>
    [TestMethod]
    public void HostConfiguration_ShouldFollowDependencyRules()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            CurrentNamespace,
            "Component dependency policy",
            $"Enforces the dependencies of the {nameof(HostConfiguration)} component");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.Cli,
                    ComponentNamespaces.WebApi,
                    ComponentNamespaces.WebAuth),
            "HostConfiguration_ShouldNotDependOn_PresentationLayer",
            "The application configurator should not have any dependencies on the presentation layer");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.CliHost,
                    ComponentNamespaces.WebHost),
            "HostConfiguration_ShouldNotDependOn_PresentationLayerHostComponents",
            $"The application configurator should not depend on any presentation layer host components such as ${nameof(WebHost)} or ${nameof(CliHost)}");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependenciesOtherThan(
                    CreateAllowedDependenciesList([
                        ComponentNamespaces.Common,
                        ComponentNamespaces.DomainEntities,
                        ComponentNamespaces.DomainServices,
                        ComponentNamespaces.AppServices,
                        ComponentNamespaces.EfCore,
                        ComponentNamespaces.EfCoreMssql,
                        ComponentNamespaces.EfCorePostgreSql
                    ])),
            "HostConfiguration_ShouldOnlyDependOn_DomainLayerAndDataAccessLayer",
            "The application configurator can only depend on application (domain) layer and data access layer");

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results)
        {
            Assert.IsTrue(result.IsSuccessful, PolicyHelper.BuildFailureMessage(result));
        }
    }

    /// <summary>
    /// Verifies that the <see cref="MindTrail.HostConfiguration"/> component's types follow the naming conventions.
    /// </summary>
    [TestMethod]
    public void HostConfiguration_ShouldFollowNamingConventions()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            CurrentNamespace,
            "Type naming policy",
            $"Enforces naming conventions for types in the {nameof(HostConfiguration)} component");

        policyDefinition
            .AddConfigNamingRule(CurrentNamespace)
            .AddExtensionNamingRule(CurrentNamespace)
            .AddHelperNamingRule(CurrentNamespace)
            .AddInterfaceNamingRule(CurrentNamespace)
            .AddProviderNamingRule(CurrentNamespace);

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results)
        {
            Assert.IsTrue(result.IsSuccessful, PolicyHelper.BuildFailureMessage(result));
        }
    }

    private static string[] CreateAllowedDependenciesList(IEnumerable<string> allowedComponents)
    {
        var allowedDependenciesList = new List<string> { CurrentNamespace };
        allowedDependenciesList.AddRange(UsingLibs);
        allowedDependenciesList.AddRange(allowedComponents);

        return allowedDependenciesList.ToArray();
    }
}