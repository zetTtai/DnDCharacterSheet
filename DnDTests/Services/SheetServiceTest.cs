using Enums;
using Interfaces;
using Models;
using Moq;
using Services;

namespace DnDTests.Services;

internal class SheetServiceTest
{
    private SheetService _service;
    private Mock<IAbilitySettingStrategy> _abilitySettingStrategyMock;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _abilitySettingStrategyMock = new Mock<IAbilitySettingStrategy>();
        _ = _abilitySettingStrategyMock
            .Setup(m => m.SetAbility(It.IsAny<Sheet>(), It.IsAny<int>(), It.IsAny<CharacterAbilities>()))
            .Returns(new Sheet());
        _service = new(_abilitySettingStrategyMock.Object);
    }

    [Test]
    public void SetAbility_ReturnsSheet()
    {
        // Arrange
        _service.SetAbilitySettingStrategy(_abilitySettingStrategyMock.Object);

        // Act
        try
        {
            _ = _service.SetAbility(new Sheet(), 3, CharacterAbilities.STR);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }

        // Assert
        Assert.Pass();
    }
}
