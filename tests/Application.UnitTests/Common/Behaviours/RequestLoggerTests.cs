using DnDCharacterSheet.Application.Common.Behaviours;
using DnDCharacterSheet.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace DnDCharacterSheet.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    // TODO: Uncomment all of this when first command created
    // private Mock<ILogger<CreateTodoItemCommand> _logger = null!;
    private Mock<IUser> _user = null!;
    private Mock<IIdentityService> _identityService = null!;

    [SetUp]
    public void Setup()
    {
        // _logger = new Mock<ILogger<CreateTodoItemCommand>>();
        _user = new Mock<IUser>();
        _identityService = new Mock<IIdentityService>();
    }

    //[Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        await Task.Delay(10);
        // _user.Setup(x => x.Id).Returns(Guid.NewGuid().ToString());

        // var requestLogger = new LoggingBehaviour<CreateTodoItemCommand>(_logger.Object, _user.Object, _identityService.Object);

        // await requestLogger.Process(new CreateTodoItemCommand { ListId = 1, Title = "title" }, new CancellationToken());

        // _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    //[Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        await Task.Delay(10);
        //var requestLogger = new LoggingBehaviour<CreateTodoItemCommand>(_logger.Object, _user.Object, _identityService.Object);

        //await requestLogger.Process(new CreateTodoItemCommand { ListId = 1, Title = "title" }, new CancellationToken());

        //_identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
    }
}
