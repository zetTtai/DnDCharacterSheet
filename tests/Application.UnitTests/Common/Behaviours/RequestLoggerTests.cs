using DnDCharacterSheet.Application.Common.Behaviours;
using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Application.Sheets.Commands.CreateSheet;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace DnDCharacterSheet.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<CreateSheetCommand>> _logger = null!;
    private Mock<IUser> _user = null!;
    private Mock<IIdentityService> _identityService = null!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<CreateSheetCommand>>();
        _user = new Mock<IUser>();
        _identityService = new Mock<IIdentityService>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        await Task.Delay(10);
        _user.Setup(x => x.Id).Returns(Guid.NewGuid().ToString());

        var requestLogger = new LoggingBehaviour<CreateSheetCommand>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new CreateSheetCommand { CharacterName = "name" }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    //[Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        await Task.Delay(10);
        var requestLogger = new LoggingBehaviour<CreateSheetCommand>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new CreateSheetCommand { CharacterName = "name" }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
    }
}
