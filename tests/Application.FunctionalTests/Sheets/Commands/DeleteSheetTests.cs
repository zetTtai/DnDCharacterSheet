using System.Net;
using DnDCharacterSheet.Application.Sheets.Commands.CreateSheet;
using DnDCharacterSheet.Application.Sheets.Commands.DeleteSheet;
using DnDCharacterSheet.Domain.Entities;

namespace DnDCharacterSheet.Application.FunctionalTests.Sheets.Commands;

using static SheetTesting;
using static Testing;

public class DeleteSheetTests : BaseTestFixture
{
    [Test]
    public async Task IfSheetDoesNotExists_ReturnStatusCodeNotFound()
    {
        // Arrange
        await RunAsDefaultUserAsync();
        var command = new DeleteSheetCommand(999);

        // Act
        var result = await SendAsync(command);

        // Assert
        result.Succeeded.Should().BeFalse();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);

    }

    [Test]
    public async Task IfUserIsNotOwner_ReturnStatusCodeForbidden()
    {
        // Arrange
        await RunAsDefaultUserAsync();
        int sheetId = await CreateSingleSheet("Sir Test Testable");
        await RunAsUserAsync("Hacker", "Hacker1234!", []);
        var command = new DeleteSheetCommand(sheetId);

        // Act
        var result = await SendAsync(command);

        // Assert
        result.Succeeded.Should().BeFalse();
        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Test]
    public async Task Succeeds()
    {
        // Arrange
        await RunAsDefaultUserAsync();
        int sheetId = await CreateSingleSheet("Sir Test Testable");
        var command = new DeleteSheetCommand(sheetId);

        // Act
        var result = await SendAsync(command);
        var sheet = await FindAsync<Sheet>(sheetId);

        // Assert
        result.Succeeded.Should().BeTrue();
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        sheet.Should().BeNull();
    }

    [Test]
    public async Task Succeeds_IfIsAdmin()
    {
        // Arrange
        await RunAsDefaultUserAsync();
        var createSheetCommand = await SendAsync(new CreateSheetCommand()
        {
            CharacterName = "Sir Test Testable"
        });
        var sheetId = createSheetCommand.Value;
        await RunAsAdministratorAsync();
        var command = new DeleteSheetCommand(sheetId);

        // Act
        var result = await SendAsync(command);
        var sheet = await FindAsync<Sheet>(sheetId);

        // Assert
        result.Succeeded.Should().BeTrue();
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        sheet.Should().BeNull();
    }
}
