using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.Domain.ValueObjects;
using MindTrail.HostConfiguration.Extensions;
using MindTrail.WebApi.Controllers;
using MindTrail.WebApi.RequestModels;
using MindTrail.WebApi.Tests.Extensions;
using MindTrail.WebApi.Tests.Factories;
using MindTrail.WebApi.Tests.Providers;

namespace MindTrail.WebApi.Tests.ApiTests;

/// <summary>
/// Tests for <see cref="PersonController"/>.
/// </summary>
[TestClass]
[DoNotParallelize]
[TestCategory("API")]
public class PersonApiTests
{
    private static CustomWebAppFactory<Program>? _appFactory;
    private static IConfiguration? _configuration;
    private static string? _apiKey;

    private readonly PersonCreationModel _personCreationModel = new()
    {
        FullName = "John Doe",
        BirthYear = 1999,
        BirthCountryId = 1,
    };

    private PersonApiProvider? _personApiProvider;

    [ClassInitialize]
    public static void Initialize(TestContext context)
    {
        _appFactory = new CustomWebAppFactory<Program>();
        _configuration = _appFactory.Services.GetRequiredService<IConfiguration>();
        _apiKey = _configuration.GetProperty("App:ApiKey");
    }

    [TestInitialize]
    public void TestInitialize()
    {
        var client = _appFactory!.CreateClient(new WebApplicationFactoryClientOptions());

        _personApiProvider = new PersonApiProvider(client, _apiKey!);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _appFactory!.ResetDatabase();
    }

    [TestMethod]
    [TestCategory("API")]
    public async Task Default_sorting_by_creation_date_desc_applied_when_getting_persons_list()
    {
        // Arrange
        await _personApiProvider!.CreatePersonAsync(_personCreationModel with { FullName = "Person A" });
        await _personApiProvider!.CreatePersonAsync(_personCreationModel with { FullName = "Person B" });
        await _personApiProvider!.CreatePersonAsync(_personCreationModel with { FullName = "Person Z" });

        var filterModel = new PersonFilterModel
        {
            PageNumber = 1,
            PageSize = 10,
        };

        // Act
        var response = await _personApiProvider!.GetPersonsAsync(filterModel);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var persons = await response.GetContentAsync<PagedDto<PersonDto>>();

        Assert.IsNotNull(persons);
        Assert.HasCount(3, persons.Items);

        var firstPerson = persons.Items.ElementAt(0);
        var secondPerson = persons.Items.ElementAt(1);
        var thirdPerson = persons.Items.ElementAt(2);

        Assert.AreEqual("Person Z", firstPerson.FullName);
        Assert.AreEqual("Person B", secondPerson.FullName);
        Assert.AreEqual("Person A", thirdPerson.FullName);
    }

    [TestMethod]
    [TestCategory("API")]
    public async Task Sorting_by_full_name_applied_when_getting_persons_list()
    {
        // Arrange
        await _personApiProvider!.CreatePersonAsync(_personCreationModel with { FullName = "Person A" });
        await _personApiProvider!.CreatePersonAsync(_personCreationModel with { FullName = "Person B" });
        await _personApiProvider!.CreatePersonAsync(_personCreationModel with { FullName = "Person Z" });

        var filterModel = new PersonFilterModel
        {
            PageNumber = 1,
            PageSize = 10,
            Sorting = "FullName ASC",
        };

        // Act
        var response = await _personApiProvider!.GetPersonsAsync(filterModel);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var persons = await response.GetContentAsync<PagedDto<PersonDto>>();

        Assert.IsNotNull(persons);
        Assert.HasCount(3, persons.Items);

        var firstPerson = persons.Items.ElementAt(0);
        var secondPerson = persons.Items.ElementAt(1);
        var thirdPerson = persons.Items.ElementAt(2);

        Assert.AreEqual("Person A", firstPerson.FullName);
        Assert.AreEqual("Person B", secondPerson.FullName);
        Assert.AreEqual("Person Z", thirdPerson.FullName);
    }

    [TestMethod]
    [TestCategory("API")]
    public async Task Sorting_by_birth_year_applied_when_getting_persons_list()
    {
        // Arrange
        await _personApiProvider!.CreatePersonAsync(
            _personCreationModel with { FullName = "Person A", BirthYear = 2003 });
        await _personApiProvider!.CreatePersonAsync(
            _personCreationModel with { FullName = "Person B", BirthYear = 2002 });
        await _personApiProvider!.CreatePersonAsync(
            _personCreationModel with { FullName = "Person Z", BirthYear = 2001 });

        var filterModel = new PersonFilterModel
        {
            PageNumber = 1,
            PageSize = 10,
            Sorting = "BirthYear ASC",
        };

        // Act
        var response = await _personApiProvider!.GetPersonsAsync(filterModel);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var persons = await response.GetContentAsync<PagedDto<PersonDto>>();

        Assert.IsNotNull(persons);
        Assert.HasCount(3, persons.Items);

        var firstPerson = persons.Items.ElementAt(0);
        var secondPerson = persons.Items.ElementAt(1);
        var thirdPerson = persons.Items.ElementAt(2);

        Assert.AreEqual("Person Z", firstPerson.FullName);
        Assert.AreEqual("Person B", secondPerson.FullName);
        Assert.AreEqual("Person A", thirdPerson.FullName);
    }

    [TestMethod]
    [TestCategory("API")]
    public async Task Search_filtering_applied_when_getting_persons_list()
    {
        // Arrange
        await _personApiProvider!.CreatePersonAsync(_personCreationModel with { FullName = "Person A" });
        await _personApiProvider!.CreatePersonAsync(_personCreationModel with { FullName = "Person B v2" });
        await _personApiProvider!.CreatePersonAsync(_personCreationModel with { FullName = "Person Z v2" });

        var filterModel = new PersonFilterModel
        {
            PageNumber = 1,
            PageSize = 10,
            Search = "v2",
        };

        // Act
        var response = await _personApiProvider!.GetPersonsAsync(filterModel);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var persons = await response.GetContentAsync<PagedDto<PersonDto>>();

        Assert.IsNotNull(persons);
        Assert.HasCount(2, persons.Items);
        Assert.AreEqual(2, persons.Total);

        var firstPerson = persons.Items.ElementAt(0);
        var secondPerson = persons.Items.ElementAt(1);

        Assert.AreEqual("Person Z v2", firstPerson.FullName);
        Assert.AreEqual("Person B v2", secondPerson.FullName);
    }

    [TestMethod]
    [TestCategory("API")]
    public async Task Pagination_applied_when_getting_persons_list()
    {
        // Arrange
        for (var i = 1; i <= 5; i++)
        {
            await _personApiProvider!.CreatePersonAsync(_personCreationModel with { FullName = $"Person {i}" });
        }

        var filterModel = new PersonFilterModel
        {
            PageNumber = 1,
            PageSize = 2,
        };

        // Act
        var response = await _personApiProvider!.GetPersonsAsync(filterModel);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var persons = await response.GetContentAsync<PagedDto<PersonDto>>();

        Assert.IsNotNull(persons);
        Assert.HasCount(2, persons.Items);
        Assert.AreEqual(5, persons.Total);

        var firstPerson = persons.Items.ElementAt(0);
        var secondPerson = persons.Items.ElementAt(1);

        Assert.AreEqual("Person 5", firstPerson.FullName);
        Assert.AreEqual("Person 4", secondPerson.FullName);
    }

    [TestMethod]
    [TestCategory("API")]
    public async Task Person_creation_rejects_birth_year_less_than_min()
    {
        // Arrange
        const int invalidBirthYear = BirthYear.MinBirthYear - 1;
        var model = _personCreationModel with { BirthYear = invalidBirthYear };

        // Act
        var response = await _personApiProvider!.CreatePersonAsync(model);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        var problemDetails = await response.GetContentAsync<ValidationProblemDetails>();
        Assert.IsNotNull(problemDetails);

        Assert.AreEqual("Birth year is outside the valid range", problemDetails.Title);
        Assert.AreEqual(
            $"The birth year must be greater than {BirthYear.MinBirthYear} and earlier than the current year. The specified value is {invalidBirthYear}).",
            problemDetails.Detail);
        Assert.AreEqual("birthYear", problemDetails.GetInvalidPropertyName());
        Assert.AreEqual(
            $"Must be greater than {BirthYear.MinBirthYear} and earlier than the current year",
            problemDetails.GetErrorDescription());
        Assert.AreEqual(BirthYear.MinBirthYear, problemDetails.GetIntParameter("minBirthYear"));
        Assert.AreEqual(invalidBirthYear, problemDetails.GetIntParameter("specifiedBirthYear"));
        Assert.AreEqual("mind_trail.birth_year_outside_range", problemDetails.GetErrorCode());
    }

    [TestMethod]
    [TestCategory("API")]
    public async Task Person_creation_rejects_too_long_full_name()
    {
        // Arrange
        var tooLongName = new string('A', PersonFullName.MaxNameLength + 1);
        var model = _personCreationModel with { FullName = tooLongName };

        // Act
        var response = await _personApiProvider!.CreatePersonAsync(model);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        var problemDetails = await response.GetContentAsync<ValidationProblemDetails>();
        Assert.IsNotNull(problemDetails);

        Assert.AreEqual("The name is too long", problemDetails.Title);
        Assert.AreEqual(
            $"The maximum length of the person's name is {PersonFullName.MaxNameLength} characters. The length of the specified name is {tooLongName.Length}.",
            problemDetails.Detail);
        Assert.AreEqual(PersonFullName.MaxNameLength, problemDetails.GetIntParameter("maxLength"));
        Assert.AreEqual("fullName", problemDetails.GetInvalidPropertyName());
        Assert.AreEqual(
            $"The maximum length is {PersonFullName.MaxNameLength} characters",
            problemDetails.GetErrorDescription());
        Assert.AreEqual("mind_trail.person_name_too_long", problemDetails.GetErrorCode());
    }

    [TestMethod]
    [TestCategory("API")]
    public async Task Person_creation_rejects_duplicate()
    {
        // Arrange
        var model = _personCreationModel with
        {
            FullName = "Person Duplicate",
            BirthYear = 2000,
        };

        var existingPerson = await (await _personApiProvider!.CreatePersonAsync(model))
            .GetContentAsync<PersonDto>();

        // Act
        var response = await _personApiProvider.CreatePersonAsync(model);

        // Assert
        Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);

        var problemDetails = await response.GetContentAsync<ValidationProblemDetails>();
        Assert.IsNotNull(problemDetails);

        Assert.AreEqual(
            $"The person with the name {model.FullName} and birth year {model.BirthYear} already exists.",
            problemDetails.Detail);
        Assert.AreEqual("Duplicate person", problemDetails.Title);
        Assert.AreEqual(existingPerson!.Id.ToString(), problemDetails.GetStringParameter("personId"));
        Assert.AreEqual(model.FullName, problemDetails.GetStringParameter("specifiedFullName"));
        Assert.AreEqual((int?)model.BirthYear, problemDetails.GetIntParameter("specifiedBirthYear"));
        Assert.AreEqual("mind_trail.person_duplicate", problemDetails.GetErrorCode());
    }
}