using Enums;
using Exceptions;
using Interfaces;
using Models;
using Moq;
using Strategies;

namespace DnDTests.Strategies;

internal class RollingDiceStrategyTest
{
    private readonly string _expectedModifier = "+4";
    private readonly string _expectedInvalidValueErrorMessage = "Value must be between 3 and 18";
    private Mock<IUtilsService> _utilsServiceMock;
    private RollingDiceStrategy _strategy;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _utilsServiceMock = new Mock<IUtilsService>();
        _ = _utilsServiceMock
            .Setup(m => m.ValueToAbilityModifier(It.IsAny<int>()))
            .Returns(_expectedModifier);
        _ = _utilsServiceMock
            .Setup(m => m.ModifyCapabilities(It.IsAny<IEnumerable<Capability>>(), It.IsAny<string>(), It.IsAny<CharacterAbilities>()))
            .Returns([]);
        _ = _utilsServiceMock
            .Setup(m => m.ModifyAbilities(It.IsAny<IEnumerable<Ability>>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CharacterAbilities>()))
            .Returns([]);
        _strategy = new RollingDiceStrategy(_utilsServiceMock.Object);
    }

    [Test]
    public void SetAbility_ValidValue_ReturnsSheet()
    {
        // Arrange
        // Act
        try
        {
            _ = _strategy.SetAbility(new Sheet(), 8, CharacterAbilities.STR);
        }
        catch (BadRequestException)
        {
            Assert.Fail();
        }

        // Assert
        Assert.Pass();
    }

    [Test]
    public void SetAbility_ValueLowerThan3_ReturnsException()
    {
        // Arrange
        string actual = "";

        // Act
        try
        {
            _ = _strategy.SetAbility(new Sheet(), 2, CharacterAbilities.STR);
        }
        catch (BadRequestException ex)
        {
            actual = ex.Message;
        }

        // Assert
        Assert.That(actual, Is.EqualTo(_expectedInvalidValueErrorMessage));
    }

    [Test]
    public void SetAbility_ValueHigherThan18_ReturnsException()
    {
        // Arrange
        string actual = "";

        // Act
        try
        {
            _ = _strategy.SetAbility(new Sheet(), 19, CharacterAbilities.STR);
        }
        catch (BadRequestException ex)
        {
            actual = ex.Message;
        }

        // Assert
        Assert.That(actual, Is.EqualTo(_expectedInvalidValueErrorMessage));
    }
}
