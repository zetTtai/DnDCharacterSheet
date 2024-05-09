namespace DnDCharacterSheet.Application.FunctionalTests.Sheets.Queries;

using DnDCharacterSheet.Application.Sheets.Commands.CreateSheet;
using Namotion.Reflection;
using static Testing;
using static SheetTesting;
using System.Net;

public class GetSheetByIdTests : BaseTestFixture
{
    [SetUp]
    public async Task AdditionalSetUp()
    {
        await TestSetUp();
        await SeedAbilitiesAndCapabilitiesDatabaseAsync();
    }

    [Test]
    public async Task ShouldDenyAnonymousUser_ReturnResponseWith401()
    {
        // Arrange
        await RunAsDefaultUserAsync();
        var sheetId = await CreateSingleSheet("Sir Test Testable");

        ResetUser();

        var query = new GetSheetByIdQuery(sheetId);

        // Act - Assert
        await ShouldDenyAnonymous(query);
    }

    [Test]
    public async Task ShouldReturnRequiredFields()
    {
        // Arrange
        await RunAsDefaultUserAsync();
        var sheetIds = await CreateSheets(3);
        var query = new GetSheetByIdQuery(sheetIds[0]);

        // Act
        var response = await SendAsync(query);
        var sheetVm = response.Value;

        // Assert
        if (sheetVm is null)
        {
            Assert.Fail("Sheet (View Model) should not be null");
            return;
        }

        sheetVm.Abilities.Should().NotBeEmpty();
        sheetVm.Abilities.Count().Should().Be(6);
        sheetVm.Abilities.First().Should().HasProperty("Id");
        sheetVm.Abilities.First().Value.Should().Be(-1);

        sheetVm.Skills.Should().NotBeEmpty();
        sheetVm.Skills.Count().Should().Be(18);
        sheetVm.Skills.First().Should().HasProperty("Id");
        sheetVm.Skills.First().Proficiency.Should().BeFalse();

        sheetVm.SavingThrows.Should().NotBeEmpty();
        sheetVm.SavingThrows.Count().Should().Be(6);
        sheetVm.SavingThrows.First().Should().HasProperty("Id");
        sheetVm.SavingThrows.First().Proficiency.Should().BeFalse();
    }

    [Test]
    public async Task IfSheetDoesNotExist_ReturnResponseWith404()
    {
        // Arrange
        await RunAsDefaultUserAsync();
        var query = new GetSheetByIdQuery(9999);

        // Act
        var response = await SendAsync(query);

        // Assert
        response.Succeeded.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task IfUserIsNotOwner_ReturnResponseWith403()
    {
        // Arrange
        var userId = await RunAsDefaultUserAsync();
        var sheetId = await CreateSingleSheet("Sir Test Testable");
        await RunAsUserAsync("Hacker", "Hacker1234!", []);
        var query = new GetSheetByIdQuery(sheetId);

        // Act
        var response = await SendAsync(query);

        // Assert
        response.Succeeded.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Test]
    public async Task Succeeds()
    {
        // Arrange
        await RunAsDefaultUserAsync();
        var sheetId = await CreateSingleSheet("Sir Test Testable");
        await CreateSingleSheet("another one");
        var query = new GetSheetByIdQuery(sheetId);

        // Act
        var response = await SendAsync(query);
        var sheetVm = response.Value;

        // Assert
        if (sheetVm is null)
        {
            Assert.Fail("Sheet (View Model) should not be null");
            return;
        }
        sheetVm.CharacterName.Should().Be("Sir Test Testable");
    }

    [Test]
    public async Task Succeeds_IfIsAdmin()
    {
        // Arrange
        await RunAsDefaultUserAsync();
        var createSheetCommand = await SendAsync(new CreateSheetCommand
        {
            CharacterName = "Sir test testable"
        });
        var sheetId = createSheetCommand.Value;
        await SendAsync(new CreateSheetCommand
        {
            CharacterName = "Another character"
        });

        await RunAsAdministratorAsync();
        var query = new GetSheetByIdQuery(sheetId);

        // Act
        var response = await SendAsync(query);
        var sheetVm = response.Value;

        // Assert
        if (sheetVm is null)
        {
            Assert.Fail("Sheet (View Model) should not be null");
            return;
        }
        sheetVm.CharacterName.Should().Be("Sir test testable");

    }
}
