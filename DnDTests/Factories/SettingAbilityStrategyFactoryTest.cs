using Enums;
using Factories;
using Interfaces;
using Moq;
using Strategies;

namespace DnDTests.Factories;

internal class SettingAbilityStrategyFactoryTest
{
    private Mock<IUtilsService> _utilsServiceMock;
    private ISettingAbilitiesStrategyFactory _factory;
    private readonly string _expectedInvalidMethodError = "Method must be 0 => RollingDice, 1 => PointBuy or 2 => StandardArray";

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _utilsServiceMock = new Mock<IUtilsService>();
        _factory = new SettingAbilityStrategyFactory(_utilsServiceMock.Object);
    }

    [Test]
    public void CreateStrategy_RollingDice_ReturnsRollingDiceStrategy()
    {
        // Arrange
        // Act
        IAbilitySettingStrategy actual = _factory.CreateStrategy(0, CharacterAbilities.STR);

        // Assert
        Assert.That(actual, Is.InstanceOf<RollingDiceStrategy>());
    }

    [Test]
    public void CreateStrategy_PointBuy_ReturnsRollingDiceStrategy()
    {
        // Arrange
        // Act
        IAbilitySettingStrategy actual = _factory.CreateStrategy((MethodsToIncreaseAbilities)1, CharacterAbilities.STR);

        // Assert
        Assert.That(actual, Is.InstanceOf<PointBuyStrategy>());
    }

    [Test]
    public void CreateStrategy_StandardArray_ReturnsRollingDiceStrategy()
    {
        // Arrange
        // Act
        IAbilitySettingStrategy actual = _factory.CreateStrategy((MethodsToIncreaseAbilities)2, CharacterAbilities.STR);

        // Assert
        Assert.That(actual, Is.InstanceOf<StandardArrayStrategy>());
    }

    [Test]
    public void CreateStrategy_InvalidMethod_ReturnsException()
    {
        // Arrange
        string actual = "";

        // Act
        try
        {
            _ = _factory.CreateStrategy((MethodsToIncreaseAbilities)3, CharacterAbilities.STR);
        }
        catch (Exception ex)
        {
            actual = ex.Message;
        }

        // Assert
        Assert.That(actual, Is.EqualTo(_expectedInvalidMethodError));
    }
}
