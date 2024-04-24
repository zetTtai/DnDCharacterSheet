using System.Net;
using DnDCharacterSheet.Application.Common.Models;
using DnDCharacterSheet.Application.Sheets.Commands.CreateSheet;

namespace DnDCharacterSheet.Application.FunctionalTests.Sheets.Commands;

using static SheetTesting;
using static Testing;

public class UpdateSheetTests : BaseTestFixture
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
        await CreateSingleSheet("Sir Test Testable");

        ResetUser();

        var command = new UpdateSheetCommand()
        {
            CharacterName = "Sir Test Modified"
        };

        // Act - Assert
        await ShouldDenyAnonymous(command);
    }

    public static IEnumerable<string> InvalidCharacterNames
    {
        get
        {
            return [new string('a', 3), new string('a', 101)];
        }
    }

    [Test]
    public async Task IfRequiredFieldsAreMissing_ReturnResponseWith400()
    {
        // Arrange
        await RunAsDefaultUserAsync();

        var command = new UpdateSheetCommand
        {
            CharacterName = string.Empty
        };

        // Act
        var response = await SendAsync(command);

        // Assert
        response.Succeeded.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Test, TestCaseSource(nameof(InvalidCharacterNames))]
    public async Task InvalidCharacterName_ReturnResponseWith400(string characterName)
    {
        // Arrange
        await RunAsDefaultUserAsync();
        var sheetId = await CreateSingleSheet("Sir Test Testable");
        var command = new UpdateSheetCommand()
        {
            CharacterName = characterName
        };

        command.Id(sheetId);

        // Act
        var response = await SendAsync(command);

        // Assert
        response.Succeeded.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

    }

    [Test]
    public async Task IfSheetDoesNotExist_ReturnResponseWith404()
    {
        // Arrange
        await RunAsDefaultUserAsync();
        var command = new UpdateSheetCommand()
        {
            CharacterName = "Sir test modified",
        };
        command.Id(999);

        // Act
        var response = await SendAsync(command);

        // Assert
        response.Succeeded.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

    }

    [Test]
    public async Task IfUserIsNotOwner_ReturnResponseWith403()
    {
        // Arrange
        var userId = await RunAsDefaultUserAsync();
        int sheetId = await CreateSingleSheet("Sir Test Testable");
        await RunAsUserAsync("Hacker", "Hacker1234!", []);

        var command = new UpdateSheetCommand()
        {
            CharacterName = "Sir test modified"
        };
        command.Id(sheetId);

        // Act
        var response = await SendAsync(command);

        // Assert
        response.Succeeded.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Test]
    public async Task Succeeds()
    {
        // Arrange
        var userId = await RunAsDefaultUserAsync();
        int sheetId = await CreateSingleSheet("Sir Test Testable");
        await CreateSingleSheet("Another character");
        var command = new UpdateSheetCommand()
        {
            CharacterName = "Sir test modified"
        };
        command.Id(sheetId);

        // Act
        var response = await SendAsync(command);
        var sheet = await FindSheetWithDetailsAsync(sheetId);

        // Assert
        response.Succeeded.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        sheet.Should().NotBeNull();

        sheet!.CharacterName.Should().Be("Sir test modified");

        AssertAuditDetails(sheet, userId, true);
    }

    [Test]
    public async Task Succeeds_IfIsAdmin()
    {
        // Arrange
        await RunAsDefaultUserAsync();
        var sheetId = await CreateSingleSheet("Sir Test Testable");
        await SendAsync(new CreateSheetCommand
        {
            CharacterName = "Another character"
        });

        var userId = await RunAsAdministratorAsync();
        var command = new UpdateSheetCommand()
        {
            CharacterName = "Sir test modified by admin"
        };
        command.Id(sheetId);

        // Act
        var response = await SendAsync(command);
        var sheet = await FindSheetWithDetailsAsync(sheetId);

        // Assert
        response.Succeeded.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        sheet.Should().NotBeNull();

        sheet!.CharacterName.Should().Be("Sir test modified by admin");

        AssertAuditDetails(sheet, userId, true);
    }

}
