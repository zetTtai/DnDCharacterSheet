using DnDCharacterSheet.Application.Common.Exceptions;
using DnDCharacterSheet.Application.Sheets.Commands.CreateSheet;
using DnDCharacterSheet.Application.Sheets.Commands.DeleteSheet;
using DnDCharacterSheet.Domain.Entities;

namespace DnDCharacterSheet.Application.FunctionalTests.Sheets.Commands;

using static Testing;

public class DeleteSheetTests : BaseTestFixture
{
    [Test]
    public async Task IfSheetDoesNotExists_ThrowsValidationException()
    {
        // Arrange
        await RunAsDefaultUserAsync();
        var command = new DeleteSheetCommand(999);

        // Act - Assert
        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task IfUserIsNotOwner_ThrowsValidationException()
    {
        // Arrange
        await RunAsDefaultUserAsync();
        var sheetId = await SendAsync(new CreateSheetCommand()
        {
            CharacterName = "sir test testable"
        });
        await RunAsUserAsync("Hacker", "Hacker1234!", []);
        var command = new DeleteSheetCommand(sheetId);

        // Act - Assert
        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task Succeeds()
    {
        // Arrange
        await RunAsDefaultUserAsync();
        var sheetId = await SendAsync(new CreateSheetCommand()
        {
            CharacterName = "sir test testable"
        });
        var command = new DeleteSheetCommand(sheetId);

        // Act
        await SendAsync(command);
        var sheet = await FindAsync<Sheet>(sheetId);

        // Assert
        sheet.Should().BeNull();
    }

    [Test]
    public async Task Succeeds_IfIsAdmin()
    {
        // Arrange
        await RunAsDefaultUserAsync();
        var sheetId = await SendAsync(new CreateSheetCommand()
        {
            CharacterName = "sir test testable"
        });

        await RunAsAdministratorAsync();
        var command = new DeleteSheetCommand(sheetId);

        // Act
        await SendAsync(command);
        var sheet = await FindAsync<Sheet>(sheetId);

        // Assert
        sheet.Should().BeNull();
    }
}
