using Interfaces;
using Models;
using Moq;
using Services;

namespace DnDTests.Services;

internal class SheetServiceTest
{
    private SheetService _service;
    private Mock<IAbilitySettingStrategy> _abilitySettingStrategyMock;
    private readonly string _strategyNotSetErrorMessage = "Strategy was not set";

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _abilitySettingStrategyMock = new Mock<IAbilitySettingStrategy>();
        _ = _abilitySettingStrategyMock
            .Setup(m => m.SetAbility(It.IsAny<Sheet>(), It.IsAny<int>()))
            .Returns(new Sheet());
        _service = new();
    }

    [Test]
    public void SetAbility_StrategySet_ReturnsSheet()
    {
        // Arrange
        _service.SetAbilitySettingStrategy(_abilitySettingStrategyMock.Object);

        // Act
        try
        {
            _ = _service.SetAbility(new Sheet(), 3);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }

        // Assert
        Assert.Pass();
    }

    [Test]
    public void SetAbility_NoStrategy_ReturnsException()
    {
        // Arrange
        string actual = "";

        // Act
        try
        {
            _ = _service.SetAbility(new Sheet(), 3);
        }
        catch (Exception ex)
        {
            actual = ex.Message;
        }

        // Assert
        Assert.That(actual, Is.EqualTo(_strategyNotSetErrorMessage));
    }
}
