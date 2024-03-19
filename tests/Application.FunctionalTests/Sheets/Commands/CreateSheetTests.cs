﻿using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CleanArchitecture.Application.FunctionalTests.Sheets.Commands;

using static Testing;
public class CreateSheetTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequiredMinimumFields()
    {
        var command = new CreateSheetCommand()
        {
            CharacterName = string.Empty,
        };
        await FluentActions.Invoking(() => 
            SendAsync(command)) 
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateSheet()
    {
        // Arrange
        await SeedAbilitiesAndCapabilitiesDatabaseAsync();

        var userId = await RunAsDefaultUserAsync();
        var command = new CreateSheetCommand
        {
            CharacterName = "Sir Test Testable"
        };

        // Act
        var id = await SendAsync(command);
        var sheet = await FindSheetWithDetailsAsync(id);

        // Assert
        sheet.Should().NotBeNull();

        sheet!.CharacterName.Should().Be(command.CharacterName);
        sheet!.SheetSavingThrows.Should().NotBeEmpty();
        sheet!.SheetSkills.Should().NotBeEmpty();
        sheet!.SheetAbilities.Should().NotBeEmpty();

        sheet.CreatedBy.Should().Be(userId);
        sheet.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}