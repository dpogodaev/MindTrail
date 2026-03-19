using System;
using Microsoft.AspNetCore.Mvc;
using NetArchTest.Rules;
using NetArchTest.Rules.Policies;

namespace MindTrail.ArchTests.Extensions;

/// <summary>
/// Policy rules extensions.
/// </summary>
public static class PolicyExtensions
{
    /// <summary>
    /// Adds a naming rule for classes used as adapters.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddAdapterNamingRule(
        this PolicyDefinition policyDefinition,
        string workingNamespace,
        string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for classes used as adapters";
        const string description = "Classes used as adapters must have names ending with the word 'Adapter'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Adapters")
                .Should().HaveNameEndingWith("Adapter");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes derived from the <see cref="Attribute"/> class.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddAttributeNamingRule(
        this PolicyDefinition policyDefinition,
        string workingNamespace,
        string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for classes derived from the Attribute class";
        const string description =
            "Classes derived from the Attribute class must have names ending with the word 'Attribute'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Attributes")
                .Should().HaveNameEndingWith("Attribute");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes used as builders.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddBuilderNamingRule(
        this PolicyDefinition policyDefinition,
        string workingNamespace,
        string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for classes used as builders";
        const string description = "Classes used as builders must have names ending with the word 'Builder'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Builders")
                .Should().HaveNameEndingWith("Builder");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes used as commands.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddCommandNamingRule(
        this PolicyDefinition policyDefinition,
        string workingNamespace,
        string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for classes used as commands";
        const string description = "Classes used as commands must have names ending with the word 'Command'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Commands")
                .Should().HaveNameEndingWith("Command");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for configuration classes.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddConfigNamingRule(
        this PolicyDefinition policyDefinition,
        string workingNamespace,
        string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for configuration classes";
        const string description = "Configuration classes must have names ending with the word 'Config'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Configs")
                .Should().HaveNameEndingWith("Config");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for context classes.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddContextNamingRule(
        this PolicyDefinition policyDefinition,
        string workingNamespace,
        string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for context classes";
        const string description = "Context classes must have names ending with the word 'Context'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Context")
                .Should().HaveNameEndingWith("Context");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes containing constants.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddConstantNamingRule(
        this PolicyDefinition policyDefinition,
        string workingNamespace,
        string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for classes containing constants";
        const string description = "Classes containing constants must have names ending with the word 'Constants'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Constants")
                .Should().HaveNameEndingWith("Constants");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes derived from the <see cref="ControllerBase"/> class.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddControllerNamingRule(
        this PolicyDefinition policyDefinition,
        string workingNamespace,
        string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for classes derived from the ControllerBase class";
        const string description =
            "Classes derived from the ControllerBase class must have names ending with the word 'Controller'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Controllers")
                .Should().HaveNameEndingWith("Controller");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes used as DTOs.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddDtoNamingRule(
        this PolicyDefinition policyDefinition,
        string workingNamespace,
        string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for classes used as DTOs";
        const string description = "Classes used as DTOs must have names ending with the word 'Dto'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Dtos")
                .Should().HaveNameMatching(@"^.*Dto(`\d+)?$");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for enum types.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddEnumNamingRule(
        this PolicyDefinition policyDefinition,
        string workingNamespace,
        string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for enum types";
        const string description = "Enum types must have names ending with the word 'Type'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Enums")
                .Should().HaveNameEndingWith("Type");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes derived from the <see cref="Exception"/> class.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddExceptionNamingRule(
        this PolicyDefinition policyDefinition,
        string workingNamespace,
        string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for classes derived from the Exception class";
        const string description =
            "Classes derived from the Exception class must have names ending with the word 'Exception'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Exceptions")
                .Should().HaveNameEndingWith("Exception");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes containing extension methods.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddExtensionNamingRule(
        this PolicyDefinition policyDefinition,
        string workingNamespace,
        string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for classes containing extensions";
        const string description = "Classes containing extensions must have names ending with the word 'Extensions'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Extensions")
                .Should().HaveNameEndingWith("Extensions");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes used as factories.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddFactoryNamingRule(
        this PolicyDefinition policyDefinition,
        string workingNamespace,
        string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for classes used as factories";
        const string description = "Classes used as factories must have names ending with the word 'Factory'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Factories")
                .Should().HaveNameEndingWith("Factory");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes used as filters.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddFilterNamingRule(
        this PolicyDefinition policyDefinition,
        string workingNamespace,
        string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for classes used as filters";
        const string description = "Classes used as filters must have names ending with the word 'Filter'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Filters")
                .Should().HaveNameEndingWith("Filter");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes used as handlers.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddHandlerNamingRule(
        this PolicyDefinition policyDefinition,
        string workingNamespace,
        string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for classes used as handlers";
        const string description = "Classes used as handlers must have names ending with the word 'Handler'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Handlers")
                .Should().HaveNameMatching(@"^.*Handler(`\d+)?$");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for helper classes.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddHelperNamingRule(
        this PolicyDefinition policyDefinition,
        string workingNamespace,
        string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for helper classes";
        const string description = "Helper classes must have names ending with the word 'Helper'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Helpers")
                .Should().HaveNameEndingWith("Helper");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for interfaces.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddInterfaceNamingRule(
        this PolicyDefinition policyDefinition,
        string workingNamespace,
        string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for interfaces";
        const string description = "Interfaces must have names beginning with the letter 'I'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Interfaces")
                .Should().HaveNameStartingWith("I");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for external dependencies.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddAbstractionNamingRule(
        this PolicyDefinition policyDefinition,
        string workingNamespace,
        string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for external dependencies";
        const string description = "External dependencies must have names beginning with the letter 'I'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Abstractions")
                .Should().HaveNameStartingWith("I");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes used as request models.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddRequestModelNamingRule(
        this PolicyDefinition policyDefinition,
        string workingNamespace,
        string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for classes used as request models";
        const string description = "Classes used as request models must have names ending with the word 'Model'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.RequestModels")
                .Should().HaveNameEndingWith("Model");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for mapping classes.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddMappingNamingRule(
        this PolicyDefinition policyDefinition,
        string workingNamespace,
        string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for mapping classes";
        const string description = "Mapping classes must have names ending with the word 'Mapping'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Mapping")
                .Should().HaveNameEndingWith("Mapping");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes containing options.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddOptionNamingRule(
        this PolicyDefinition policyDefinition,
        string workingNamespace,
        string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for classes containing options";
        const string description = "Classes containing options must have names ending with the word 'Options'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Options")
                .Should().HaveNameEndingWith("Options");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes used as providers.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddProviderNamingRule(
        this PolicyDefinition policyDefinition,
        string workingNamespace,
        string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for classes used as providers";
        const string description = "Classes used as providers must have names ending with the word 'Provider'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Providers")
                .Should().HaveNameEndingWith("Provider");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes used as repositories.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddRepositoryNamingRule(
        this PolicyDefinition policyDefinition,
        string workingNamespace,
        string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for classes used as repositories";
        const string description = "Classes used as repositories must have names ending with the word 'Repository'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Repositories")
                .Should().HaveNameEndingWith("Repository");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes used as services.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddServiceNamingRule(
        this PolicyDefinition policyDefinition,
        string workingNamespace,
        string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for classes used as services";
        const string description = "Classes used as services must have names ending with the word 'Service'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Services")
                .Should().HaveNameEndingWith("Service");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes containing settings.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddSettingNamingRule(
        this PolicyDefinition policyDefinition,
        string workingNamespace,
        string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for classes containing settings";
        const string description = "Classes containing settings must have names ending with the word 'Settings'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Settings")
                .Should().HaveNameEndingWith("Settings");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes used as validators.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddValidatorNamingRule(
        this PolicyDefinition policyDefinition,
        string workingNamespace,
        string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for classes used as validators";
        const string description = "Classes used as validators must have names ending with the word 'Validator'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Validators")
                .Should().HaveNameEndingWith("Validator");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    private static void AddExceptionsToRule(this ConditionList conditions, string[] exceptionsToRule)
    {
        if (exceptionsToRule == null)
        {
            return;
        }

        foreach (var exceptionToRule in exceptionsToRule)
        {
            conditions.Or().HaveName(exceptionToRule);
        }
    }
}