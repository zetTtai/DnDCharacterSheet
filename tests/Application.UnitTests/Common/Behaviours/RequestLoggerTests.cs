using CleanArchitecture.Application.Common.Behaviours;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Sheets.Commands.CreateSheet;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace CleanArchitecture.Application.UnitTests.Common.Behaviours;

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
        _ = _user.Setup(x => x.Id).Returns(Guid.NewGuid().ToString());

        LoggingBehaviour<CreateSheetCommand> requestLogger = new(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new CreateSheetCommand { CharacterName = "Test" }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        LoggingBehaviour<CreateSheetCommand> requestLogger = new(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new CreateSheetCommand { CharacterName = "Test" }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
    }
}
