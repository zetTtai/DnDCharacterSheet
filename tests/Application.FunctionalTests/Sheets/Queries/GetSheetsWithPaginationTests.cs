namespace DnDCharacterSheet.Application.FunctionalTests.Sheets.Queries;

using DnDCharacterSheet.Application.Common.Exceptions;
using DnDCharacterSheet.Application.Sheets.Queries.GetSheets;
using Namotion.Reflection;
using static Testing;
using static SheetTesting;
using System.Net;

public class GetSheetsWithPaginationTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnRequiredFields()
    {
        // Arrange
        await RunAsDefaultUserAsync();
        await CreateSheets(1);
        await RunAsAdministratorAsync();
        var query = new GetSheetsWithPaginationQuery();

        // Act
        var result = await SendAsync(query);

        // Assert
        var sheet = result.Value!.Items.First();

        sheet.CreatedByName.Should().NotBeNull();
        sheet.CreatedBy.Should().NotBeNull();
        sheet.LastModifiedByName.Should().NotBeNull();
        sheet.LastModifiedBy.Should().NotBeNull();
        sheet.Should().HasProperty("Id");
        sheet.Should().HasProperty("Created");
        sheet.Should().HasProperty("LastModified");
    }

    [Test]
    public async Task IfUserIsNotAdmin_ReturnStatusCodeUnauthorized()
    {
        // Arrange
        await CreateSheets(1);
        await RunAsUserAsync("NormalUser", "Other1234!", []);
        var query = new GetSheetsWithPaginationQuery();

        // Act 
        var result = await SendAsync(query);

        // Assert
        result.Succeeded.Should().BeFalse();
        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        // Arrange
        var query = new GetSheetsWithPaginationQuery();

        // Act 
        var result = await SendAsync(query);

        // Assert
        result.Succeeded.Should().BeFalse();
        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Test]
    public async Task Succeeds_ReturnPaginatedListOfSheets()
    {
        // Arrange
        await CreateSheets(20);
        await RunAsAdministratorAsync();
        var query = new GetSheetsWithPaginationQuery();

        // Act
        var result = await SendAsync(query);
        var paginatedList = result.Value;

        // Assert
        result.Succeeded.Should().BeTrue();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        paginatedList!.TotalCount.Should().Be(20);
        paginatedList!.TotalPages.Should().Be(2);
    }
}
